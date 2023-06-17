using System.Data.Common;
using Dapper;
using RadiusDomain.Entities;
using RadiusDomain.Exceptions;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.SGBDs.Interfaces;

namespace RadiusDomain.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ISgbd _sgbd;

    private readonly IRadAttributeRepository _radAttributeRepository;

    private const int MaxTask = 50;

    public UserRepository(ISgbd sgbd, IRadAttributeRepository radAttributeRepository)
    {
        _sgbd = sgbd;
        _radAttributeRepository = radAttributeRepository;
    }

    public async Task InsertMany(List<User> users)
    {
        var radCheckDbOperationsOperations = new List<RadCheckDbOperation>();

        for (var i = 0; i < users.Count; i += MaxTask)
        {
            var tasksQt = int.Min(MaxTask, users.Count - i);
            var tasks = new Task<RadCheckDbOperation>[tasksQt];

            for (var j = 0; j < tasksQt; j++) tasks[j] = GetRadCheckDbOperation(users[i + j]);

            var operations = await Task.WhenAll(tasks);
            radCheckDbOperationsOperations.AddRange(operations);
        }

        var attrsToRemove = new List<object>();
        var attrsToUpdate = new List<object>();
        var attrsToInsert = new List<object>();

        foreach (var op in radCheckDbOperationsOperations)
        {
            if (op.ToRemove != null) attrsToRemove.AddRange(op.ToRemove.Select(attr => new { attr.Id }));

            if (op.ToUpdate != null)
            {
                attrsToUpdate.AddRange(op.ToUpdate.Select(attr =>
                    new { attr.Id, Attribute = attr.Name, attr.Op, attr.Value }));
            }

            if (op.ToInsert != null)
            {
                attrsToInsert.AddRange(op.ToInsert.Select(attr =>
                    new { Username = op.Owner, Attribute = attr.Name, attr.Op, attr.Value }));
            }
        }

        const string rmSql = "DELETE FROM radcheck where id = @Id";
        const string updateSql =
            "UPDATE radcheck SET attribute = @Attribute, op = @Op, value = @Value WHERE id = @Id";
        const string insertSql =
            "INSERT INTO radcheck (username, attribute, op, value) VALUES (@Username, @Attribute, @Op, @Value)";

        await using var connection = _sgbd.GetDbConnection();
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            if (attrsToRemove.Any()) await connection.ExecuteAsync(rmSql, attrsToRemove, transaction);
            if (attrsToUpdate.Any()) await connection.ExecuteAsync(updateSql, attrsToUpdate, transaction);
            if (attrsToInsert.Any()) await connection.ExecuteAsync(insertSql, attrsToInsert, transaction);

            await transaction.CommitAsync();
        }
        catch (DbException e)
        {
            await transaction.RollbackAsync();
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    public Task<User?> GetByName(string name)
    {
        const string sql = "SELECT id, attribute, op, value FROM radcheck WHERE username = @UserName";

        using var connection = _sgbd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { UserName = name });

            var attrs = new List<RadiusAttribute>();

            while (result.Read())
            {
                var attr = new RadiusAttribute
                {
                    Id = result.GetInt32(0), Name = result.GetString(1), Op = result.GetString(2),
                    Value = result.GetString(3)
                };

                attrs.Add(attr);
            }

            return Task.FromResult(attrs.Count > 0 ? new User { Username = name, Attributes = attrs } : null);
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    public Task<List<User>> GetAll()
    {
        const string sql = "SELECT id, username, attribute, op, value FROM radcheck";

        using var connection = _sgbd.GetDbConnection();
        try
        {
            var result = connection.ExecuteReader(sql);

            var users = new Dictionary<string, User>();

            while (result.Read())
            {
                var attr = new RadiusAttribute
                {
                    Id = result.GetInt32(0), Name = result.GetString(2), Op = result.GetString(3),
                    Value = result.GetString(4)
                };
                var username = result.GetString(1);

                users.TryAdd(username, new User { Username = username, Attributes = new List<RadiusAttribute>() });

                var user = users[username];
                user.Attributes.Add(attr);
            }

            return Task.FromResult(users.Values.ToList());
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    /**
     * <exception cref="BaseException{T}"></exception>
     */
    private async Task<RadCheckDbOperation> GetRadCheckDbOperation(User user)
    {
        var userDb = await GetByName(user.Username);

        if (userDb == null) return new RadCheckDbOperation(user.Username, null, null, user.Attributes);

        var currentAttrs = new Dictionary<string, RadiusAttribute>();

        foreach (var dbAttribute in userDb.Attributes)
        {
            var groupCode = _radAttributeRepository.GetGroupCodeByAttribute(dbAttribute.Name)!;

            currentAttrs.Add(groupCode, dbAttribute);
        }

        var toRemove = new List<RadiusAttribute>();
        var toUpdate = new List<RadiusAttribute>();
        var toInsert = new List<RadiusAttribute>();

        foreach (var newAttr in user.Attributes)
        {
            var newAttrGroupCode = _radAttributeRepository.GetGroupCodeByAttribute(newAttr.Name)!;

            if (!currentAttrs.ContainsKey(newAttrGroupCode))
            {
                toInsert.Add(newAttr);
                continue;
            }

            var currentAttrCollision = currentAttrs[newAttrGroupCode];

            if (newAttr.Name.Equals(currentAttrCollision.Name))
            {
                newAttr.Id = currentAttrCollision.Id;
                toUpdate.Add(newAttr);
                continue;
            }

            toRemove.Add(currentAttrCollision);
            toInsert.Add(newAttr);
        }

        return new RadCheckDbOperation(user.Username, toRemove, toUpdate, toInsert);
    }
}

internal class RadCheckDbOperation
{
    public RadCheckDbOperation(string owner, List<RadiusAttribute>? toRemove, List<RadiusAttribute>? toUpdate,
        List<RadiusAttribute>? toInsert)
    {
        Owner = owner;
        ToRemove = toRemove;
        ToUpdate = toUpdate;
        ToInsert = toInsert;
    }

    public string Owner { get; init; }

    public List<RadiusAttribute>? ToRemove { get; init; }

    public List<RadiusAttribute>? ToUpdate { get; init; }

    public List<RadiusAttribute>? ToInsert { get; init; }
}
using System.Data.Common;
using Dapper;
using RadiusDomain.Entities;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.SGBDs.Interfaces;

namespace RadiusDomain.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ISgbd _sgbd;

    private readonly IRadAttributeMerge _attributeMerge;

    public GroupRepository(ISgbd sgbd, IRadAttributeMerge attributeMerge)
    {
        _sgbd = sgbd;
        _attributeMerge = attributeMerge;
    }

    /**
     * Create a new group if it doesn't exist, if it does, update it.
     */
    public async Task<int> Insert(Group group)
    {
        if (group.Attributes == null) return 0;

        var groupDb = await GetByKey(group.Name);

        var attrDbOp = _attributeMerge.Merge(groupDb != null ? groupDb.Attributes! : new List<RadiusAttribute>(),
            group.Attributes);

        var attrsToRemove = attrDbOp.ToRemove?.Select(attr => new { attr.Id });
        var attrsToUpdate =
            attrDbOp.ToUpdate?.Select(attr => new { attr.Id, Attribute = attr.Name, attr.Op, attr.Value });
        var attrsToInsert = attrDbOp.ToInsert?.Select(attr =>
            new { GroupName = group.Name, Attribute = attr.Name, attr.Op, attr.Value });

        const string rmSql = "DELETE FROM radgroupcheck where id = @Id";
        const string updateSql =
            "UPDATE radgroupcheck SET attribute = @Attribute, op = @Op, value = @Value WHERE id = @Id";
        const string insertSql =
            "INSERT INTO radgroupcheck (groupname, attribute, op, value) VALUES (@GroupName, @Attribute, @Op, @Value)";

        await using var connection = _sgbd.GetDbConnection();
        await connection.OpenAsync();
        
        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            var rowAffected = 0;

            if (attrsToRemove != null) rowAffected += await connection.ExecuteAsync(rmSql, attrsToRemove, transaction);
            if (attrsToUpdate != null)
                rowAffected += await connection.ExecuteAsync(updateSql, attrsToUpdate, transaction);
            if (attrsToInsert != null)
                rowAffected += await connection.ExecuteAsync(insertSql, attrsToInsert, transaction);

            await transaction.CommitAsync();

            return rowAffected;
        }
        catch (DbException e)
        {
            await transaction.RollbackAsync();
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    public Task<Group?> GetByKey(string groupName)
    {
        const string sql = "SELECT id, attribute, op, value FROM radgroupcheck WHERE groupname = @GroupName";

        using var connection = _sgbd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { GroupName = groupName });

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

            return Task.FromResult(attrs.Any() ? new Group { Name = groupName, Attributes = attrs } : null);
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    public Task<List<Group>> GetAll()
    {
        const string sql = "SELECT id, groupname, attribute, op, value FROM radgroupcheck";

        using var connection = _sgbd.GetDbConnection();
        try
        {
            var result = connection.ExecuteReader(sql);

            var groups = new Dictionary<string, Group>();

            while (result.Read())
            {
                var attr = new RadiusAttribute
                {
                    Id = result.GetInt32(0), Name = result.GetString(2), Op = result.GetString(3),
                    Value = result.GetString(4)
                };
                var groupName = result.GetString(1);

                groups.TryAdd(groupName, new Group { Name = groupName, Attributes = new List<RadiusAttribute>() });

                groups[groupName].Attributes!.Add(attr);
            }

            return Task.FromResult(groups.Values.ToList());
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }
}
using System.Data.Common;
using Dapper;
using RadiusDomain.Entities;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.SGBDs.Interfaces;

namespace RadiusDomain.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ISgbd _sgbd;

    public GroupRepository(ISgbd sgbd)
    {
        _sgbd = sgbd;
    }

    public Task<int> Insert(Group group)
    {
        var attrs = group.Attributes.Select(attribute => new
        {
            GroupName = group.Name, Attribute = attribute.Name, op = attribute.Op,
            attribute.Value
        });

        const string sql =
            "INSERT INTO radgroupcheck (groupname, attribute, op, value) VALUES (@GroupName, @Attribute, @op, @Value)";

        using var connection = _sgbd.GetDbConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            var rowAffected = connection.Execute(sql, attrs, transaction);
            transaction.Commit();

            return Task.FromResult(rowAffected);
        }
        catch (DbException e)
        {
            transaction.Rollback();
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    public Task<Group?> GetByName(string name)
    {
        const string sql = "SELECT id, attribute, op, value FROM radgroupcheck WHERE groupname = @GroupName";

        using var connection = _sgbd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { GroupName = name });

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

            return Task.FromResult(attrs.Count > 0 ? new Group { Name = name, Attributes = attrs } : null);
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

                var group = groups[groupName];
                group.Attributes.Add(attr);
            }

            return Task.FromResult(groups.Values.ToList());
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }
}
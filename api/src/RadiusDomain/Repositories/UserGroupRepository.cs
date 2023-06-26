using System.Data.Common;
using Dapper;
using RadiusDomain.Entities;
using RadiusDomain.Repositories.Interfaces;
using RadiusDomain.SGBDs.Interfaces;

namespace RadiusDomain.Repositories;

public class UserGroupRepository : IUserGroupRepository
{
    private readonly ISgbd _sgbd;

    public UserGroupRepository(ISgbd sgbd)
    {
        _sgbd = sgbd;
    }

    public Task<int> Insert(UserGroup userGroup)
    {
        const string sql =
            "INSERT INTO radusergroup (username, groupname, priority) VALUES (@Username, @GroupName, @Priority)";

        var uGroup = new { userGroup.User!.Username, GroupName = userGroup.Group!.Name, userGroup.Priority };

        using var connection = _sgbd.GetDbConnection();

        try
        {
            var rowAffected = connection.Execute(sql, uGroup);

            return Task.FromResult(rowAffected);
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }

    public Task<List<UserGroup>> GetAllByUsername(string username)
    {
        const string sql = "SELECT id, groupname, priority WHERE username = @Username";

        var user = new { Username = username };

        using var connection = _sgbd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, user);

            var userGroups = new List<UserGroup>();

            while (result.Read())
            {
                var uGroup = new UserGroup
                    { Id = result.GetInt32(0), GroupName = result.GetString(1), Priority = result.GetInt32(2) };

                userGroups.Add(uGroup);
            }

            return Task.FromResult(userGroups);
        }
        catch (DbException e)
        {
            throw _sgbd.ExceptionHandler.Handler(e);
        }
    }
}
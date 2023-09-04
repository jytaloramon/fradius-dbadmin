using System.Data.Common;
using Dapper;
using FradminDomain.Entities;
using FradminDomain.Repositories.Interfaces;
using FradminDomain.SGBDs;
using FradminDomain.ValueObjects;

namespace FradminDomain.Repositories;

public class AdminGroupRepository : IAdminGroupRepository
{
    private readonly SgbdBase _bd;

    public AdminGroupRepository(SgbdBase bd)
    {
        _bd = bd;
    }

    public Task<AdminGroup?> GetById(int id)
    {
        const string sql =
            "SELECT admg_name, rule_id FROM tb_admin_group INNER JOIN tb_rule ON admg_id = rule_admg_id WHERE admg_id = @Id";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { Id = id });

            if (!result.Read()) return Task.FromResult<AdminGroup?>(null);

            var name = result.GetString(0);
            var rules = new List<Rules>() { (Rules)result.GetInt32(1) };

            while (result.Read()) rules.Add((Rules)result.GetInt32(1));

            return Task.FromResult<AdminGroup?>(new AdminGroup
                { Id = id, Name = name, Rules = new HashSet<Rules>(rules) });
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<AdminGroup?> GetByName(string name)
    {
        const string sql =
            "SELECT admg_id, rule_id FROM tb_admin_group INNER JOIN tb_rule ON admg_id = rule_admg_id WHERE admg_name = @Name";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { Name = name });

            if (!result.Read()) return Task.FromResult<AdminGroup?>(null);

            var id = result.GetInt32(0);
            var rules = new List<Rules>() { (Rules)result.GetInt32(1) };

            while (result.Read()) rules.Add((Rules)result.GetInt32(1));

            return Task.FromResult<AdminGroup?>(new AdminGroup
                { Id = id, Name = name, Rules = new HashSet<Rules>(rules) });
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<IEnumerable<AdminGroup>> GetAll()
    {
        const string sql =
            "SELECT admg_id, admg_name, rule_id FROM tb_admin_group INNER JOIN tb_rule ON admg_id = rule_admg_id";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql);

            var groups = new Dictionary<int, AdminGroup>();

            while (result.Read())
            {
                var idGroup = result.GetInt32(0);

                if (!groups.ContainsKey(idGroup))
                {
                    var group = new AdminGroup
                        { Id = idGroup, Name = result.GetString(1), Rules = new HashSet<Rules>() };

                    groups.Add(idGroup, group);
                }

                groups[idGroup].Rules.Add((Rules)result.GetInt16(2));
            }

            return Task.FromResult<IEnumerable<AdminGroup>>(groups.Values.ToList());
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<int> Save(AdminGroup adminGroup)
    {
        const string sqlInsertGroup = "INSERT INTO tb_admin_group (admg_name) VALUES (@Name) RETURNING admg_id";
        const string sqlInsertRule = "INSERT INTO tb_rule (rule_id, rule_admg_id) VALUES (@Id, @IdGroup)";

        using var connection = _bd.GetDbConnection();
        connection.Open();

        using var transaction = connection.BeginTransaction();

        try
        {
            var idGen = connection.ExecuteScalar<short>(sqlInsertGroup, adminGroup, transaction);
            var rulesToInsert = adminGroup.Rules.Select(rule => new { Id = (short)rule, IdGroup = idGen });
            var affectedRow = connection.Execute(sqlInsertRule, rulesToInsert, transaction);

            transaction.Commit();

            return Task.FromResult(affectedRow + 1);
        }
        catch (DbException e)
        {
            transaction.Rollback();
            throw _bd.ExceptionHandler.Handler(e);
        }
    }
}
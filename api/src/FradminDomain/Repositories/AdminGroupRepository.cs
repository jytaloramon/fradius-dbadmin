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

    public Task<AdminGroup>? GetByName(string name)
    {
        const string sql =
            "SELECT admg_id, rule_id FROM tb_admin_group INNER JOIN tb_rule ON admg_id = rule_admg_id WHERE admg_name = @Name";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { Name = name });

            if (!result.Read()) return Task.FromResult<AdminGroup>(null!);

            var id = result.GetInt16(0);
            var rules = new List<Rules>() { (Rules)result.GetInt16(1) };

            while (result.Read())
            {
                rules.Add((Rules)result.GetInt16(1));
            }

            return Task.FromResult(new AdminGroup { Id = id, Name = name, Rules = new HashSet<Rules>(rules) });
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<int> Insert(AdminGroup adminGroup)
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

            var rowAffected = connection.Execute(sqlInsertRule, rulesToInsert, transaction);

            transaction.Commit();

            return Task.FromResult(rowAffected + 1);
        }
        catch (DbException e)
        {
            transaction.Rollback();
            throw _bd.ExceptionHandler.Handler(e);
        }
    }
}
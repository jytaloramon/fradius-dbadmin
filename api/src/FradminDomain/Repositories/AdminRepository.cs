using System.Data.Common;
using Dapper;
using FradminDomain.Entities;
using FradminDomain.Repositories.Interfaces;
using FradminDomain.SGBDs;

namespace FradminDomain.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly SgbdBase _bd;

    public AdminRepository(SgbdBase bd)
    {
        _bd = bd;
    }

    public Task<Admin?> GetById(int id)
    {
        const string sql =
            "SELECT adm_admg_id, adm_username, adm_email, adm_is_active FROM tb_admin WHERE adm_id = @Id";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { Id = id });

            if (!result.Read()) return Task.FromResult<Admin?>(null);

            var admin = new Admin
            {
                Id = id,
                Group = new AdminGroup { Id = result.GetInt16(0) },
                Username = result.GetString(1),
                Email = result.GetString(2),
                IsActive = result.GetBoolean(3),
            };

            return Task.FromResult<Admin?>(admin);
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<Admin?> GetByUsername(string username)
    {
        const string sql =
            "SELECT adm_id, adm_admg_id, adm_email, adm_is_active FROM tb_admin WHERE adm_username = @Username";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql, new { Username = username });

            if (!result.Read()) return Task.FromResult<Admin?>(null);

            var admin = new Admin
            {
                Id = result.GetInt32(0),
                Group = new AdminGroup { Id = result.GetInt16(1) },
                Username = username,
                Email = result.GetString(2),
                IsActive = result.GetBoolean(3),
            };

            return Task.FromResult<Admin?>(admin);
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<IEnumerable<Admin>> GetAll()
    {
        const string sql = "SELECT adm_id, adm_admg_id, adm_username, adm_email, adm_is_active FROM tb_admin";

        using var connection = _bd.GetDbConnection();

        try
        {
            var result = connection.ExecuteReader(sql);

            var admins = new List<Admin>();

            while (result.Read())
            {
                var adm = new Admin
                {
                    Id = result.GetInt32(0),
                    Group = new AdminGroup { Id = result.GetInt16(1) },
                    Username = result.GetString(2),
                    Email = result.GetString(3),
                    IsActive = result.GetBoolean(4),
                };

                admins.Add(adm);
            }

            return Task.FromResult<IEnumerable<Admin>>(admins);
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }

    public Task<int> Save(Admin admin)
    {
        const string sql =
            "INSERT INTO tb_admin (adm_username, adm_email, adm_password_hash, adm_admg_id, adm_is_active)" +
            "VALUES (@Username, @Email, @Password, @AdminGroupId, @IsActive)";

        var entity = new
            { admin.Username, admin.Email, admin.Password, AdminGroupId = admin.Group.Id, IsActive = admin.IsActive };

        using var connection = _bd.GetDbConnection();

        try
        {
            return Task.FromResult(connection.Execute(sql, entity));
        }
        catch (DbException e)
        {
            throw _bd.ExceptionHandler.Handler(e);
        }
    }
}
using FradminDomain.Entities;

namespace FradminDomain.Repositories.Interfaces;

public interface IAdminRepository : IReadByIdRepository<Admin>, IReadAllRepository<Admin>, IWriteRepository<Admin>
{
    /**
     * Get a Entity by username.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>Entity found, or null if not.</returns>
     */
    public Task<Admin?> GetByUsername(string username);
}
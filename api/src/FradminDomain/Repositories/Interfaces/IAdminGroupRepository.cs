using FradminDomain.Entities;

namespace FradminDomain.Repositories.Interfaces;

public interface IAdminGroupRepository : IReadAllRepository<AdminGroup>, IReadByIdRepository<AdminGroup>,
    IWriteRepository<AdminGroup>
{
    /**
     * Get AdminGroup by name.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup found, or null if not.</returns>
     */
    public Task<AdminGroup?> GetByName(string name);
}
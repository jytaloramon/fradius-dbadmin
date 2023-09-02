using FradminDomain.Entities;

namespace FradminDomain.Repositories.Interfaces;

public interface IAdminGroupRepository
{
    /**
     * Get AdminGroup by name.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup found, or null if not.</returns>
     */
    public Task<AdminGroup>? GetByName(string name);

    /**
     * Get all AdminGroup.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup List.</returns>
     */
    public Task<List<AdminGroup>> GetAll();

    /**
     * Add a new AdminGroup.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup added.</returns>
     */
    public Task<AdminGroup> Insert(AdminGroup adminGroup);
}
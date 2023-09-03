namespace FradminDomain.Repositories.Interfaces;

public interface IReadByIdRepository<T>
{
    /**
     * Get a Entity by ID.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>Entity found, or null if not.</returns>
     */
    public Task<T?> GetById(int id);
}
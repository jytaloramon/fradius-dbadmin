namespace FradminDomain.Repositories.Interfaces;

public interface IReadAllRepository<T>
{
    /**
     * Get all Entities.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>Entities set.</returns>
     */
    public Task<IEnumerable<T>> GetAll();
}
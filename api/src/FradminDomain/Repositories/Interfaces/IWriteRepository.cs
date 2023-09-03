namespace FradminDomain.Repositories.Interfaces;

public interface IWriteRepository<T>
{
    /**
     * Add a new Entity.
     * <exception cref="Exception"></exception>
     * <exception cref="BaseException"></exception>
     * <returns>The number of rows affected.</returns>
     */
    public Task<int> Save(T e);
}
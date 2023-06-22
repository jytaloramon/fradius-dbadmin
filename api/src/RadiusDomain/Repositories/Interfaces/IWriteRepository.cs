namespace RadiusDomain.Repositories.Interfaces;

public interface IWriteRepository<in T>
{
    /**
     * Insert an object into the repository.
     * <returns>The number of rows affected.</returns>
     */
    public Task<int> Insert(T obj);
}
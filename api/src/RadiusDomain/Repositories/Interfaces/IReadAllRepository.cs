namespace RadiusDomain.Repositories.Interfaces;

public interface IReadAllRepository<T>
{
    /**
     * Get all objects from repository.
     * <returns>List of objects</returns>
     */
    public Task<List<T>> GetAll();
}
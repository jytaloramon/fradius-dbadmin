namespace RadiusDomain.Repositories.Interfaces;

public interface IReadFilteredByKey<T>
{
    /**
     * Get an object from repository by key.
     * <returns>An object if found, or null if not.</returns>
     */
    public Task<T?> GetByKey(string key);
}
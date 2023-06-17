using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IUserRepository
{
    public Task InsertMany(List<User> users);

    public Task<User?> GetByName(string name);

    public Task<List<User>> GetAll();
}
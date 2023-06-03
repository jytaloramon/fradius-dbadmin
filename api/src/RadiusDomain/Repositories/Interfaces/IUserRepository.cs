using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IUserRepository
{
    public void InsertMany(IEnumerable<User> users);
}
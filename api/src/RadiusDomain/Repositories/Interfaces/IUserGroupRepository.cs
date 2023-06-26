using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IUserGroupRepository
{
    public Task<int> Insert(UserGroup userGroup);

    public Task<List<UserGroup>> GetAllByUsername(string username);
}
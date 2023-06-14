using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IGroupRepository
{
    public Task<int> Insert(Group group);

    public Task<Group?> GetByName(string name);

    public Task<List<Group>> GetAll();
}
using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IGroupRepository:IReadAllRepository<Group>, IWriteRepository<Group>
{
    public Task<Group?> GetByUsername(string username);
}
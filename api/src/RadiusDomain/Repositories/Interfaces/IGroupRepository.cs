using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IGroupRepository : IReadAllRepository<Group>, IReadFilteredByKey<Group>, IWriteRepository<Group>
{
}
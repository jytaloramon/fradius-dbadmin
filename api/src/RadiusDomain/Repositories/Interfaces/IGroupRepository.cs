

using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IGroupRepository
{
    public Group? GetByName(string name);
}
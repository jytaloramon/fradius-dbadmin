using FradminDomain.Entities;

namespace FradminDomain.Repositories.Interfaces;

public interface IAdminGroupRepository
{
    public Task<AdminGroup>? GetByName(string name);

    public Task<AdminGroup> Insert(AdminGroup adminGroup);
}
using FradminDomain.DTOs;
using FradminDomain.Factories.Interfaces;
using FradminDomain.Repositories.Interfaces;
using FradminDomain.UseCases.Interfaces;

namespace FradminDomain.UseCases;

public class AdminGroupUseCase : IAdminGroupUseCase
{
    private readonly IAdminGroupFactory _factory;

    private readonly IAdminGroupRepository _repository;

    public AdminGroupUseCase(IAdminGroupFactory factory, IAdminGroupRepository repository)
    {
        _factory = factory;
        _repository = repository;
    }

    public async Task<AdminGroupFullDto> Add(AdminGroupNewDto groupDto)
    {
        var group = _factory.Create(groupDto.Name, groupDto.Rules.ToHashSet());
        var groupCreated = await _repository.Insert(group);

        return new AdminGroupFullDto(groupCreated.Id, groupCreated.Name, groupCreated.Rules.ToArray());
    }
}
using System.Collections.Immutable;
using FradminDomain.DTOs;
using FradminDomain.Exceptions;
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

    public async Task<AdminGroupFullDto?> GetById(int id)
    {
        var group = await _repository.GetById(id);

        return group != null ? new AdminGroupFullDto(group.Id, group.Name, group.Rules.ToArray()) : null;
    }

    public async Task<List<AdminGroupFullDto>> GetAll()
    {
        var allGroups = await _repository.GetAll();

        return allGroups.Select(group => new AdminGroupFullDto(group.Id, group.Name, group.Rules.ToArray())).ToList();
    }

    public async Task<AdminGroupFullDto> Add(AdminGroupAddDto groupDto)
    {
        var group = _factory.Create(groupDto.Name, groupDto.Rules.ToHashSet());

        if (await _repository.Save(group) < 1)
        {
            throw new GenericException(new Dictionary<string, object>()
            {
                ["message"] = "Error in server."
            }.ToImmutableDictionary());
        }

        var groupCreated = await _repository.GetByName(group.Name);

        return new AdminGroupFullDto(groupCreated!.Id, groupCreated.Name, groupCreated.Rules.ToArray());
    }
}
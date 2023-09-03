using System.Collections.Immutable;
using FradminDomain.DTOs;
using FradminDomain.Entities;
using FradminDomain.Exceptions;
using FradminDomain.Factories.Interfaces;
using FradminDomain.Repositories.Interfaces;
using FradminDomain.UseCases.Interfaces;

namespace FradminDomain.UseCases;

public class AdminUseCase : IAdminUseCase
{
    private readonly IAdminFactory _factory;

    private readonly IAdminRepository _repository;

    public AdminUseCase(IAdminFactory factory, IAdminRepository repository)
    {
        _factory = factory;
        _repository = repository;
    }

    public async Task<AdminFullDto> Add(AdminAddDto adminDto)
    {
        var admin = _factory.Create(adminDto.Username, adminDto.Email,
            adminDto.Password ?? Guid.NewGuid().ToString()[0..12], new AdminGroup { Id = adminDto.GroupId },
            adminDto.IsActive);

        if (await _repository.Save(admin) < 1)
        {
            throw new GenericException(new Dictionary<string, object>()
            {
                ["message"] = "Error in server."
            }.ToImmutableDictionary());
        }

        var adminCreated = await _repository.GetByUsername(admin.Username);

        return new AdminFullDto(adminCreated!.Id, adminCreated.Username, adminCreated.Email, adminCreated.Group.Id,
            adminCreated.IsActive);
    }
}
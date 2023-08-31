using FradminDomain.DTOs;
using FradminDomain.Exceptions;

namespace FradminDomain.UseCases.Interfaces;

public interface IAdminGroupUseCase
{
    /**
     * Create a new AdminGroup.
     * <exception cref="BaseException"></exception>
     * <returns>Group Created.</returns>
     */
    public Task<AdminGroupFullDto> Add(AdminGroupNewDto groupDto);
}
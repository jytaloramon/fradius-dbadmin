using FradminDomain.DTOs;

namespace FradminDomain.UseCases.Interfaces;

public interface IAdminUseCase
{
    /**
     * Add a new Admin.
     * <exception cref="Exception"></exception>.
     * <exception cref="BaseException"></exception>
     * <returns>Admin Created.</returns>
     */
    public Task<AdminFullDto> Add(AdminAddDto adminDto);
}
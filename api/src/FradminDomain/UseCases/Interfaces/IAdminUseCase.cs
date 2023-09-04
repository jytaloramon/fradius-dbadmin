using FradminDomain.DTOs;

namespace FradminDomain.UseCases.Interfaces;

public interface IAdminUseCase
{
    /**
    * Get all Admin.
    * <exception cref="Exception"></exception>.
    * <exception cref="BaseException"></exception>
    * <returns>Admin List.</returns>
    */
    public Task<List<AdminFullDto>> GetAll();

    /**
     * Add a new Admin.
     * <exception cref="Exception"></exception>.
     * <exception cref="BaseException"></exception>
     * <returns>Admin Created.</returns>
     */
    public Task<AdminFullDto> Add(AdminAddDto adminDto);
}
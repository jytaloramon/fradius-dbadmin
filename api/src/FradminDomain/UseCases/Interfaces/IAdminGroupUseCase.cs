using FradminDomain.DTOs;

namespace FradminDomain.UseCases.Interfaces;

public interface IAdminGroupUseCase
{
    /**
     * Get AdminGroup by Id.
     * <exception cref="Exception"></exception>.
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup found, or null if not.</returns>
     */
    public Task<AdminGroupFullDto?> GetById(int id);

    /**
     * Get all AdminGroup.
     * <exception cref="Exception"></exception>.
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup List.</returns>
     */
    public Task<List<AdminGroupFullDto>> GetAll();

    /**
     * Create a new AdminGroup.
     * <exception cref="Exception"></exception>.
     * <exception cref="BaseException"></exception>
     * <returns>AdminGroup Created.</returns>
     */
    public Task<AdminGroupFullDto> Add(AdminGroupAddDto groupDto);
}
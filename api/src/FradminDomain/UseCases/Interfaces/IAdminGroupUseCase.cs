using FradminDomain.DTOs;

namespace FradminDomain.UseCases.Interfaces;

public interface IAdminGroupUseCase
{
    /**
     * Get all AdminGroup.
     * <exception cref="Exception"></exception>.
     * <returns>AdminGroup List.</returns>
     */
    public Task<List<AdminGroupFullDto>> GetAll();

    /**
     * Create a new AdminGroup.
     * <exception cref="Exception"></exception>.
     * <returns>AdminGroup Created.</returns>
     */
    public Task<AdminGroupFullDto> Add(AdminGroupNewDto groupDto);
}
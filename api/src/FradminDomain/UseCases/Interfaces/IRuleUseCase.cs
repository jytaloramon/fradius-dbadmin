using FradminDomain.DTOs;

namespace FradminDomain.UseCases.Interfaces;

public interface IRuleUseCase
{
    /**
     * Get all "Rule" Groups.
     * <returns>Rule Group List.</return>
     */
    public Task<RuleGroupDto[]> GetAllGroups();
}

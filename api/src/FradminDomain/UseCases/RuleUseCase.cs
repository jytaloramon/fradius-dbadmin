using FradminDomain.DTOs;
using FradminDomain.UseCases.Interfaces;
using FradminDomain.ValueObjects;

namespace FradminDomain.UseCases;

public class RuleUseCase : IRuleUseCase
{
    private static readonly RuleGroupDto[] _rules = new[]{
        new RuleGroupDto("Management", new []{
            new RuleDto((short) Rules.ManagementAdmin, "admin"),
            new RuleDto((short) Rules.ManagementGroup, "group")
        })
    };

    public Task<RuleGroupDto[]> GetAllGroups()
    {
        return Task.FromResult(_rules);
    }
}

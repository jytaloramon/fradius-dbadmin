namespace FradminDomain.DTOs;

public record RuleDto(short Id, string Name);

public record RuleGroupDto(string Name, RuleDto[] Rules);

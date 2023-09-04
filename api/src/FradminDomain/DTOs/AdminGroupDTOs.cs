using FradminDomain.ValueObjects;

namespace FradminDomain.DTOs;

public record AdminGroupAddDto(string Name, Rules[] Rules);

public record AdminGroupFullDto(int Id, string Name, Rules[] Rules);
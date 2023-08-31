using FradminDomain.ValueObjects;

namespace FradminDomain.DTOs;

public record AdminGroupNewDto(string Name, Rules[] Rules);

public record AdminGroupFullDto(short Id, string Name, Rules[] Rules);
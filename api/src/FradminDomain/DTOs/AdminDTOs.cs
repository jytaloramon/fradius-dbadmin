namespace FradminDomain.DTOs;

public record AdminAddDto(string Username, string Email, string? Password, short GroupId, bool IsActive);

public record AdminFullDto(int Id, string Username, string Email, short GroupId, bool IsActive);
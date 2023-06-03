using RadiusDomain.DTOs.Attribute;

namespace RadiusDomain.DTOs.User;

public record UserPushInDto(string Username, List<AttributeDto> Attributes, List<UserGroupDto> Groups);
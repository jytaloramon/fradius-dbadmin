using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IRadAttributeRepository
{
    public string? GetGroupCodeByAttribute(string attribute);

    public RadGroup? GetRadGroupByCode(string code);

    public RadGroup[] GetAllGroups();
}
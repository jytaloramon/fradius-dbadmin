using RadiusDomain.Entities;

namespace RadiusDomain.Repositories.Interfaces;

public interface IRadAttributeMerge
{
    public RadAttributeDbOperation Merge(List<RadiusAttribute> currentAttrs, List<RadiusAttribute> newAttrs);
}
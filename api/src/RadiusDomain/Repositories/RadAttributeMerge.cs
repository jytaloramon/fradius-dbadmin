using RadiusDomain.Entities;
using RadiusDomain.Repositories.Interfaces;

namespace RadiusDomain.Repositories;

public class RadAttributeMerge : IRadAttributeMerge
{
    private readonly IRadAttributeRepository _radAttributeRepository;

    public RadAttributeMerge(IRadAttributeRepository radAttributeRepository)
    {
        _radAttributeRepository = radAttributeRepository;
    }

    public RadAttributeDbOperation Merge(List<RadiusAttribute> currentAttrs, List<RadiusAttribute> newAttrs)
    {
        if (!currentAttrs.Any()) return new RadAttributeDbOperation(null, null, newAttrs);

        var currentAttrsMap = new Dictionary<string, RadiusAttribute>();

        foreach (var attr in currentAttrs)
        {
            var groupCode = _radAttributeRepository.GetGroupCodeByAttribute(attr.Name)!;
            currentAttrsMap.TryAdd(groupCode, attr);
        }

        var toRemove = new List<RadiusAttribute>();
        var toUpdate = new List<RadiusAttribute>();
        var toInsert = new List<RadiusAttribute>();

        foreach (var newAttr in newAttrs)
        {
            var newAttrGroupCode = _radAttributeRepository.GetGroupCodeByAttribute(newAttr.Name)!;

            if (!currentAttrsMap.ContainsKey(newAttrGroupCode))
            {
                toInsert.Add(newAttr);
                continue;
            }

            currentAttrsMap.Remove(newAttrGroupCode, out var currentAttrCollision);

            if (newAttr.Name.Equals(currentAttrCollision!.Name))
            {
                newAttr.Id = currentAttrCollision.Id;
                toUpdate.Add(newAttr);
                continue;
            }

            toRemove.Add(currentAttrCollision);
            toInsert.Add(newAttr);
        }

        toRemove.AddRange(currentAttrsMap.Values.ToList());

        return new RadAttributeDbOperation(toRemove.Any() ? toRemove : null, toUpdate.Any() ? toUpdate : null,
            toInsert.Any() ? toInsert : null);
    }
}
using System.Collections.Immutable;
using RadiusDomain.Entities;
using RadiusDomain.Repositories.Interfaces;

namespace RadiusDomain.Repositories;

public class RadAttributeRepository : IRadAttributeRepository
{
    private readonly ImmutableDictionary<string, RadGroup> _groups;

    private readonly ImmutableDictionary<string, string> _attributes;
    
    /**
     * <exception cref="Exception"></exception>
     */
    public RadAttributeRepository(IEnumerable<RadGroup> groupAttributes)
    {
        var tempGroups = new Dictionary<string, RadGroup>();
        var tempAttrs = new Dictionary<string, string>();

        foreach (var group in groupAttributes)
        {
            if (tempGroups.ContainsKey(group.Code)) throw new Exception($"Duplicate Group, code ({group.Code}).");

            tempGroups.Add(group.Code, group);

            foreach (var attr in group.Attributes)
            {
                if (tempAttrs.TryGetValue(attr, out var groupCode))
                {
                    throw new Exception(
                        $"Duplicate Attribute ({attr}) in the groups \"{groupCode}\" and \"{group.Code}\"");
                }

                tempAttrs.Add(attr, group.Code);
            }
        }

        _attributes = tempAttrs.ToImmutableDictionary();
        _groups = tempGroups.ToImmutableDictionary();
    }

    public string? GetGroupCodeByAttribute(string attribute)
    {
        return _attributes.TryGetValue(attribute, out var groupCode) ? groupCode : null;
    }

    public RadGroup? GetRadGroupByCode(string code)
    {
        return _groups.TryGetValue(code, out var group) ? group : null;
    }

    public RadGroup[] GetAllGroups()
    {
        return _groups.Values.ToArray();
    }
}
using RadiusDomain.Entities;

namespace RadiusDomain.Repositories;

public class RadAttributeDbOperation
{
    public RadAttributeDbOperation(List<RadiusAttribute>? toRemove, List<RadiusAttribute>? toUpdate,
        List<RadiusAttribute>? toInsert)
    {
        ToRemove = toRemove;
        ToUpdate = toUpdate;
        ToInsert = toInsert;
    }

    public List<RadiusAttribute>? ToRemove { get; init; }

    public List<RadiusAttribute>? ToUpdate { get; init; }

    public List<RadiusAttribute>? ToInsert { get; init; }
}
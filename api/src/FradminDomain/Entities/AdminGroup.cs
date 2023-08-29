using FradminDomain.ValueObjects;

namespace FradminDomain.Entities;

public class AdminGroup
{
    public short Id { get; set; }

    public string Name { get; set; } = "";

    public HashSet<Rules> Rules { get; set; } = new HashSet<Rules>();
}
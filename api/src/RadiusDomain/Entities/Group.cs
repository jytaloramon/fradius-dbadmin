namespace RadiusDomain.Entities;

public class Group
{
    public string Name { get; set; } = "";

    public List<RadiusAttribute>? Attributes { get; set; }

    public List<UserGroup>? Groups { get; set; }
}
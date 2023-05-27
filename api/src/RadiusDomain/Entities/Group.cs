namespace RadiusDomain.Entities;

public class Group
{
    public string Name { get; set; } = "";

    public List<RadiusAttribute> Attributes { get; set; } = new List<RadiusAttribute>();

    public List<UserGroup> Groups { get; set; } = new List<UserGroup>();
}
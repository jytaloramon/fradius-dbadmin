namespace RadiusDomain.Entities;

public class User
{
    public string Username { get; set; } = "";

    public List<RadiusAttribute> Attributes { get; set; } = new List<RadiusAttribute>();

    public List<UserGroup>? Groups { get; set; }
}
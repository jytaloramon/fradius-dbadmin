namespace RadiusDomain.Entities;

public class UserGroup
{
    public int Id { get; init; }

    // "Foreign Key"
    public string UserUsername { get; set; } = "";

    public User? User { get; set; } = null;

    // "Foreign Key"
    public string GroupName { get; set; } = "";

    public Group? Group { get; set; } = null;

    public int Priority { get; set; }
}
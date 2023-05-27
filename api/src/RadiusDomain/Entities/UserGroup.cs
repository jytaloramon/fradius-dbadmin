namespace RadiusDomain.Entities;

public class UserGroup
{
    public int Id { get; set; }

    public User User { get; set; } = new User();

    public Group Group { get; set; } = new Group();

    public int Priority { get; set; }
}
namespace FradminDomain.Entities;

public class Admin
{
    public int Id { get; set; }

    public string Username { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

    public AdminGroup Group { get; set; } = new AdminGroup();

    public bool isActive { get; set; } = true;
}

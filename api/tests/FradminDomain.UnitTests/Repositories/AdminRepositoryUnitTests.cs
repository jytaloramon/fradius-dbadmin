using FradminDomain.Entities;
using FradminDomain.Repositories;
using FradminDomain.SGBDs;

namespace FradminDomain.UnitTest.Repositories;

public class AdminRepositoryUnitTests
{
    private readonly AdminRepository _repository = new(
        new PostgresConnection("psqldb", "5432", "dev", "dev", "db_frdbadmin"));

    [Fact]
    public async Task GetById_NotFound_ReturnNull()
    {
        var adminFound = await _repository.GetById(0);

        Assert.Null(adminFound);
    }

    [Fact]
    public async Task GetById_Found_ReturnTheEntity()
    {
        var admin = new Admin
        {
            Username = Guid.NewGuid().ToString()[0..16],
            Email = Guid.NewGuid().ToString()[0..16],
            Password = "12345678",
            Group = new AdminGroup { Id = 1 },
            IsActive = true
        };

        await _repository.Save(admin);
        var idFind = (await _repository.GetByUsername(admin.Username))!.Id;
        var adminFound = await _repository.GetById(idFind);

        Assert.NotNull(adminFound);
        Assert.True(adminFound.Id > 0);
        Assert.Equal(admin.Username, adminFound.Username);
        Assert.Equal(admin.Email, adminFound.Email);
        Assert.Equal(admin.IsActive, adminFound.IsActive);
    }

    [Fact]
    public async Task GetByUsername_NotFound_ReturnNull()
    {
        var adminFound = await _repository.GetByUsername("");

        Assert.Null(adminFound);
    }

    [Fact]
    public async Task GetByUsername_Found_ReturnTheEntity()
    {
        var admin = new Admin
        {
            Username = Guid.NewGuid().ToString()[0..16],
            Email = Guid.NewGuid().ToString()[0..16],
            Password = "12345678",
            Group = new AdminGroup { Id = 1 },
            IsActive = true
        };

        await _repository.Save(admin);
        var adminFound = await _repository.GetByUsername(admin.Username);

        Assert.NotNull(adminFound);
        Assert.True(adminFound.Id > 0);
        Assert.Equal(admin.Username, adminFound.Username);
        Assert.Equal(admin.Email, adminFound.Email);
        Assert.Equal(admin.IsActive, adminFound.IsActive);
    }

    [Fact]
    public async Task GetAll_InsertAdmin_ReturnNonEmptyList()
    {
        var admin = new Admin
        {
            Username = Guid.NewGuid().ToString()[0..16],
            Email = Guid.NewGuid().ToString()[0..16],
            Password = "12345678",
            Group = new AdminGroup { Id = 1 },
            IsActive = true
        };

        await _repository.Save(admin);
        var allAdmins = await _repository.GetAll();

        Assert.NotEmpty(allAdmins);
    }

    [Fact]
    public async Task Save_Admin_ReturnTheEntityWithIdGreaterThan0()
    {
        var admin = new Admin
        {
            Username = Guid.NewGuid().ToString()[0..16],
            Email = Guid.NewGuid().ToString()[0..16],
            Password = "12345678",
            Group = new AdminGroup { Id = 1 },
            IsActive = true
        };

        var affectedRow = await _repository.Save(admin);

        Assert.True(affectedRow > 0);
    }
}
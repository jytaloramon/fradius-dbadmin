using System.Collections.Immutable;
using FradminDomain.DTOs;
using FradminDomain.Entities;
using FradminDomain.Exceptions;
using FradminDomain.Factories.Interfaces;
using FradminDomain.Repositories.Interfaces;
using FradminDomain.UseCases;
using Moq;

namespace FradminDomain.UnitTest.UseCases;

public class AdminUseCaseUnitTests
{
    [Fact]
    public async Task Add_FactoryCreateThrowEntityValidationException_ThrowEntityValidationException()
    {
        var factoryMock = new Mock<IAdminFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<AdminGroup>(), It.IsAny<bool>()))
            .Throws(new EntityValidationException(new Dictionary<string, object>() { ["Username"] = "Test" }
                .ToImmutableDictionary()));

        var repositoryMock = new Mock<IAdminRepository>();

        var useCase = new AdminUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<EntityValidationException>(async () =>
        {
            await useCase.Add(new AdminAddDto("", "", null, 0, true));
        });

        Assert.True(except.Errors.ContainsKey("Username"));
    }

    [Fact]
    public async Task Add_RepositoryInsertThrowGenericException_ThrowGenericException()
    {
        var factoryMock = new Mock<IAdminFactory>();

        var repositoryMock = new Mock<IAdminRepository>();
        repositoryMock.Setup(repository => repository.Insert(It.IsAny<Admin>()))
            .Throws(new GenericException(new Dictionary<string, object>() { ["message"] = "Test" }
                .ToImmutableDictionary()));

        var useCase = new AdminUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<GenericException>(async () =>
        {
            await useCase.Add(new AdminAddDto("", "", null, 0, true));
        });

        Assert.True(except.Errors.ContainsKey("message"));
    }

    [Fact]
    public async Task Add_RepositoryInsertReturn0_ThrowGenericException()
    {
        var adminValidMock = new Admin
        {
            Username = "a", Email = "a@a", Group = new AdminGroup { Id = 1 }, Password = "012345678912", IsActive = true
        };

        var factoryMock = new Mock<IAdminFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsRegex(".{12,}"),
                It.IsAny<AdminGroup>(), It.IsAny<bool>()))
            .Returns(adminValidMock);

        var repositoryMock = new Mock<IAdminRepository>();
        repositoryMock.Setup(repository => repository.Insert(It.IsAny<Admin>()))
            .Returns(Task.FromResult(0));

        var useCase = new AdminUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<GenericException>(async () =>
        {
            await useCase.Add(new AdminAddDto(adminValidMock.Username, adminValidMock.Email,
                adminValidMock.Password, 1, adminValidMock.IsActive));
        });

        Assert.True(except.Errors.ContainsKey("message"));
    }

    [Fact]
    public async Task Add_OkEntity_ReturnTheEntityCreated()
    {
        var adminValidMock = new Admin
        {
            Username = "a", Email = "a@a", Group = new AdminGroup { Id = 1 }, Password = "012345678912", IsActive = true
        };

        var adminCreatedMock = new Admin
        {
            Id = 1000, Username = "a", Email = "a@a", Group = new AdminGroup { Id = 1 }, Password = "012345678912",
            IsActive = true
        };

        var factoryMock = new Mock<IAdminFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsRegex(".{12,}"),
                It.IsAny<AdminGroup>(), It.IsAny<bool>()))
            .Returns(adminValidMock);

        var repositoryMock = new Mock<IAdminRepository>();
        repositoryMock.Setup(repository => repository.Insert(adminValidMock))
            .Returns(Task.FromResult(1));
        repositoryMock.Setup(repository => repository.GetByUsername("a"))
            .Returns(Task.FromResult<Admin?>(adminCreatedMock));

        var useCase = new AdminUseCase(factoryMock.Object, repositoryMock.Object);

        var adminCreated = await useCase.Add(new AdminAddDto(adminValidMock.Username, adminValidMock.Email,
            adminValidMock.Password, 1, adminValidMock.IsActive));

        Assert.Equal(adminCreatedMock.Id, adminCreated.Id);
        Assert.Equal(adminCreatedMock.Username, adminCreated.Username);
        Assert.Equal(adminCreatedMock.Email, adminCreated.Email);
        Assert.Equal(adminCreatedMock.Group.Id, adminCreated.GroupId);
        Assert.Equal(adminCreatedMock.IsActive, adminCreated.IsActive);
    }

    [Fact]
    public async Task Add_OkEntityWithPasswordNull_ReturnTheEntityCreated()
    {
        var adminValidMock = new Admin
        {
            Username = "a", Email = "a@a", Group = new AdminGroup { Id = 1 }, Password = "012345678912", IsActive = true
        };

        var adminCreatedMock = new Admin
        {
            Id = 1000, Username = "a", Email = "a@a", Group = new AdminGroup { Id = 1 }, Password = "012345678912",
            IsActive = true
        };

        var factoryMock = new Mock<IAdminFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsRegex(".{12,}"),
                It.IsAny<AdminGroup>(), It.IsAny<bool>()))
            .Returns(adminValidMock);

        var repositoryMock = new Mock<IAdminRepository>();
        repositoryMock.Setup(repository => repository.Insert(adminValidMock))
            .Returns(Task.FromResult(1));
        repositoryMock.Setup(repository => repository.GetByUsername("a"))
            .Returns(Task.FromResult<Admin?>(adminCreatedMock));

        var useCase = new AdminUseCase(factoryMock.Object, repositoryMock.Object);

        var adminCreated = await useCase.Add(new AdminAddDto(adminValidMock.Username, adminValidMock.Email,
            null, 1, adminValidMock.IsActive));

        Assert.Equal(adminCreatedMock.Id, adminCreated.Id);
        Assert.Equal(adminCreatedMock.Username, adminCreated.Username);
        Assert.Equal(adminCreatedMock.Email, adminCreated.Email);
        Assert.Equal(adminCreatedMock.Group.Id, adminCreated.GroupId);
        Assert.Equal(adminCreatedMock.IsActive, adminCreated.IsActive);
    }
}
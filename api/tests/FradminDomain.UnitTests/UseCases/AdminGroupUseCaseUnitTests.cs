using System.Collections.Immutable;
using FradminDomain.DTOs;
using FradminDomain.Entities;
using FradminDomain.Exceptions;
using FradminDomain.Factories.Interfaces;
using FradminDomain.Repositories.Interfaces;
using FradminDomain.UseCases;
using FradminDomain.ValueObjects;
using Moq;

namespace FradminDomain.UnitTest.UseCases;

public class AdminGroupUseCaseUnitTests
{
    [Fact]
    public async Task GetById_RepositoryGetAllThrowGenericException_ThrowGenericException()
    {
        var factoryMock = new Mock<IAdminGroupFactory>();

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.GetById(It.IsAny<short>()))
            .Throws(new GenericException(new Dictionary<string, object>() { ["message"] = "Test" }
                .ToImmutableDictionary()));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<GenericException>(async () => { await useCase.GetById(0); });

        Assert.True(except.Errors.ContainsKey("message"));
    }

    [Fact]
    public async Task GetById_NotFound_ReturnNull()
    {
        var factoryMock = new Mock<IAdminGroupFactory>();

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.GetById(It.IsAny<short>()))
            .Returns(Task.FromResult<AdminGroup?>(null));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var actual = await useCase.GetById(0);

        Assert.Null(actual);
    }

    [Fact]
    public async Task GetById_Found_ReturnTheEntity()
    {
        var groupReturned = new AdminGroup { Id = 1000, Name = Guid.NewGuid().ToString()[..16], Rules = new HashSet<Rules>(new[] { (Rules)1 }) };

        var factoryMock = new Mock<IAdminGroupFactory>();

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.GetById(It.IsAny<short>()))
            .Returns(Task.FromResult<AdminGroup?>(groupReturned));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var actual = await useCase.GetById(1000);

        Assert.NotNull(actual);
        Assert.Equal(groupReturned.Id, actual.Id);
        Assert.Equal(groupReturned.Name, actual.Name);
        Assert.True(groupReturned.Rules.SetEquals(actual.Rules));
    }

    [Fact]
    public async Task GetAll_RepositoryGetAllThrowGenericException_ThrowGenericException()
    {
        var factoryMock = new Mock<IAdminGroupFactory>();

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.GetAll())
            .Throws(new GenericException(new Dictionary<string, object>() { ["message"] = "Test" }
                .ToImmutableDictionary()));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<GenericException>(async () => { await useCase.GetAll(); });

        Assert.True(except.Errors.ContainsKey("message"));
    }

    [Fact]
    public async Task GetAll_WithNonEmptyList_ReturnAList()
    {
        var groupsRepo = new List<AdminGroup>(){
            new() { Id = 1000, Name = "1000", Rules = new HashSet<Rules>(){} },
            new() { Id = 2000, Name = "2000", Rules = new HashSet<Rules>(){} },
        };

        var factoryMock = new Mock<IAdminGroupFactory>();

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.GetAll())
            .Returns(Task.FromResult(groupsRepo));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var groups = await useCase.GetAll();

        Assert.Equal(2, groups.Count);
        Assert.True(groups.FindIndex(group => group.Id == 1000) != -1);
        Assert.True(groups.FindIndex(group => group.Id == 2000) != -1);
    }

    [Fact]
    public async Task Add_FactoryCreateThrowEntityValidationException_ThrowEntityValidationException()
    {
        var factoryMock = new Mock<IAdminGroupFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<HashSet<Rules>>()))
            .Throws(new EntityValidationException(new Dictionary<string, object>() { ["Name"] = "Test" }
                .ToImmutableDictionary()));

        var repositoryMock = new Mock<IAdminGroupRepository>();

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<EntityValidationException>(async () =>
        {
            await useCase.Add(new AdminGroupNewDto("", Array.Empty<Rules>()));
        });

        Assert.True(except.Errors.ContainsKey("Name"));
    }

    [Fact]
    public async Task Add_RepositoryInsertThrowGenericException_ThrowGenericException()
    {
        var factoryMock = new Mock<IAdminGroupFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<HashSet<Rules>>()))
            .Returns(new AdminGroup { Name = "", Rules = new HashSet<Rules>() });

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.Insert(It.IsAny<AdminGroup>()))
            .Throws(new GenericException(new Dictionary<string, object>() { ["message"] = "Test" }
                .ToImmutableDictionary()));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);

        var except = await Assert.ThrowsAsync<GenericException>(async () =>
        {
            await useCase.Add(new AdminGroupNewDto("", Array.Empty<Rules>()));
        });

        Assert.True(except.Errors.ContainsKey("message"));
    }

    [Fact]
    public async Task Add_ValidEntity_ReturnTheEntityWithId1000()
    {
        const string name = "GroupTest";
        var rules = new HashSet<Rules>(new[] { (Rules)1, (Rules)2 });

        var factoryMock = new Mock<IAdminGroupFactory>();
        factoryMock.Setup(factory => factory.Create(It.IsAny<string>(), It.IsAny<HashSet<Rules>>()))
            .Returns(new AdminGroup { Name = name, Rules = rules });

        const short expectedId = 1000;

        var repositoryMock = new Mock<IAdminGroupRepository>();
        repositoryMock.Setup(repository => repository.Insert(It.IsAny<AdminGroup>()))
            .Returns(Task.FromResult<AdminGroup>(new AdminGroup { Id = expectedId, Name = name, Rules = rules }));

        var useCase = new AdminGroupUseCase(factoryMock.Object, repositoryMock.Object);
        var adminGroup = await useCase.Add(new AdminGroupNewDto(name, rules.ToArray()));

        Assert.Equal(expectedId, adminGroup.Id);
    }
}
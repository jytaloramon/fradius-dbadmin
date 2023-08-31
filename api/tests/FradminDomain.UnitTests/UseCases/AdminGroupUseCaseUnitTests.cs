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
    public async Task Add_ValidEntity_ReturnEntityWithId1000()
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
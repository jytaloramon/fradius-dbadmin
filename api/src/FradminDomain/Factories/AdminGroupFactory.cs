using FluentValidation;
using FradminDomain.Entities;
using FradminDomain.Factories.Interfaces;
using FradminDomain.ValueObjects;

namespace FradminDomain.Factories;

public class AdminGroupFactory : BaseFactory<AdminGroup>, IAdminGroupFactory
{
    public AdminGroupFactory(AbstractValidator<AdminGroup> validator) : base(validator)
    {
    }

    public AdminGroup Create(string name, HashSet<Rules> rules)
    {
        var adminGroup = new AdminGroup { Name = name, Rules = rules };

        var result = RunValidator(adminGroup, "Name", "Rules");

        if (result.IsValid) return adminGroup;

        throw CreateEntityValidationException(result.Errors);
    }
}
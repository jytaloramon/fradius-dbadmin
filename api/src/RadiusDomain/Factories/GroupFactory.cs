using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Factories.Interfaces;

namespace RadiusDomain.Factories;

public class GroupFactory : BaseFactory<Group>, IGroupFactory
{
    public GroupFactory(AbstractValidator<Group> validator) : base(validator)
    {
    }

    public Group Create(string name)
    {
        var group = new Group { Name = name };

        var resultValidation = RunValidator(group, "Name");

        if (resultValidation.IsValid) return group;

        var errorsPairs = resultValidation.Errors.GroupBy(vf => vf.PropertyName)
            .Select(vfGp => new KeyValuePair<string, object>(vfGp.Key,
                vfGp.Select(failure => new ErrorMessage(failure.ErrorCode, failure.ErrorMessage)).ToArray()));

        throw CreateEntityValidationException(errorsPairs);
    }

    public Group Create(string name, List<RadiusAttribute> attributes)
    {
        var group = new Group { Name = name, Attributes = attributes };

        var resultValidation = RunValidator(group, "Name", "Attributes");

        if (resultValidation.IsValid) return group;

        var errorsPairs = resultValidation.Errors.GroupBy(vf => vf.PropertyName)
            .Select(vfGp => new KeyValuePair<string, object>(vfGp.Key,
                vfGp.Select(failure => new ErrorMessage(failure.ErrorCode, failure.ErrorMessage)).ToArray()));

        throw CreateEntityValidationException(errorsPairs);
    }
}
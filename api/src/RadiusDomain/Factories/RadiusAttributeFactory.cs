using FluentValidation;
using RadiusDomain.Entities;
using RadiusDomain.Factories.Interfaces;

namespace RadiusDomain.Factories;

public class RadiusAttributeFactory : BaseFactory<RadiusAttribute>, IRadiusAttributeFactory
{
    public RadiusAttributeFactory(AbstractValidator<RadiusAttribute> validator) : base(validator)
    {
    }

    public RadiusAttribute Create(string name, string op, string value)
    {
        var attribute = new RadiusAttribute { Name = name, Op = op, Value = value };
        var resultValidation = RunValidator(attribute, "Name", "Op", "Value");

        if (resultValidation.IsValid) return attribute;

        var errorsPairs = resultValidation.Errors.GroupBy(vf => vf.PropertyName)
            .Select(vfGp => new KeyValuePair<string, object>(vfGp.Key,
                vfGp.Select(failure => new ErrorMessage(failure.ErrorCode, failure.ErrorMessage)).ToArray()));

        var pairs = new List<KeyValuePair<string, object>>(errorsPairs) { new KeyValuePair<string, object>("Key", attribute.Name) };

        throw CreateEntityException(pairs);
    }
}
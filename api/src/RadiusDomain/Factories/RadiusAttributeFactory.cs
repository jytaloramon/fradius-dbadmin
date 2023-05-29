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

        if (!resultValidation.IsValid)
        {
            throw CreateEntityException(resultValidation.Errors);
        }

        return attribute;
    }
}
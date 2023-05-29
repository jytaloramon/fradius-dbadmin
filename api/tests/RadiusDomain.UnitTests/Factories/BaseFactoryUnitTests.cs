using FluentValidation;

namespace RadiusDomain.UnitTests.Factories;

public abstract class BaseFactoryUnitTests<T>
{
    protected readonly InlineValidator<T> ValidatorAllValid;

    protected readonly InlineValidator<T> ValidatorAllInvalid;

    protected BaseFactoryUnitTests(InlineValidator<T> validatorAllValid, InlineValidator<T> validatorAllInvalid)
    {
        ValidatorAllValid = validatorAllValid;
        ValidatorAllInvalid = validatorAllInvalid;
    }
}
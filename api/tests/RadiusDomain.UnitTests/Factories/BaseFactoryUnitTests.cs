using FluentValidation;

namespace RadiusDomain.UnitTests.Factories;

public abstract class BaseFactoryUnitTests<T>
{
    protected BaseFactoryUnitTests(InlineValidator<T> validatorInValid, InlineValidator<T> validatorValid)
    {
        ValidatorInValid = validatorInValid;
        ValidatorValid = validatorValid;
    }

    protected InlineValidator<T> ValidatorInValid { get; init; }

    protected InlineValidator<T> ValidatorValid { get; init; }
}
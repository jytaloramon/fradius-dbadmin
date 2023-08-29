using FluentValidation;

namespace FradminDomain.UnitTest.Factories;

public abstract class BaseUnitTestsFactory <T>
{
    public abstract InlineValidator<T> GetValidatorAllInvalid();
    
    public abstract InlineValidator<T> GetValidatorAllValid();
}
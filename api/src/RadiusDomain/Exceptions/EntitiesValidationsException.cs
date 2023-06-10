namespace RadiusDomain.Exceptions;

public class EntitiesValidationsException : BaseMultiException<EntityValidationException>
{
    public EntitiesValidationsException(EntityValidationException[] errors) : base(errors)
    {
    }
}
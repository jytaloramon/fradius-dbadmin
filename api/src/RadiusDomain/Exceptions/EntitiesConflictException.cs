namespace RadiusDomain.Exceptions;

public class EntitiesConflictException : BaseMultiException<EntityConflictException>
{
    public EntitiesConflictException(EntityConflictException[] errors) : base(errors)
    {
    }
}
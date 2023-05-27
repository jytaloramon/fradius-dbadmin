using RadiusDomain.Entities;

namespace RadiusDomain.ValueObjects;

public static class ErrorCatalog
{
    public static readonly ErrorMessage RequiredProperty = new ErrorMessage("0", "required property");
    
    public static readonly ErrorMessage FieldExceeded = new ErrorMessage("1", "field exceeded");
}
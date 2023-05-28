namespace RadiusDomain.ValueObjects;

public static class RegexPatternCatalog
{
    public const string RadiusOp = @"^([:=+!><]?=|[<>]|[=!][~*])$";
}
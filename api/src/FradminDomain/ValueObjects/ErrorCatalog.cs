namespace FradminDomain.ValueObjects;

public record ErroFormat(string Code, string Message);

public static class ErrorCatalog
{
    public static readonly ErroFormat OutOfRange = new ErroFormat("AAAAA", "AAAAA");

    public static readonly ErroFormat OutOfRangeMin = new ErroFormat("AAAAB", "AAAAB");

    public static readonly ErroFormat OutOfRangeMax = new ErroFormat("AAAAC", "AAAAC");

    public static readonly ErroFormat Required = new ErroFormat("AAAAD", "AAAAD");

    public static readonly ErroFormat NotFound = new ErroFormat("AAAAE", "AAAAE");

    public static readonly ErroFormat InvalidFormat = new ErroFormat("AAAAF", "AAAAF");
}
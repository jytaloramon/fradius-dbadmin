namespace RadiusDomain.Exceptions;

public abstract class BaseMultiException<T> : Exception
{
    protected BaseMultiException(T[] errors)
    {
        Errors = errors;
    }

    public T[] Errors { get; init; }
}
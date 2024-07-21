namespace FamilySync.Core.Abstractions.Exceptions;

public class TooManyRequestsException : FamilySyncException
{
    public TooManyRequestsException()
    {
    }

    public TooManyRequestsException(string? message) : base(message)
    {
    }

    public TooManyRequestsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
namespace FamilySync.Core.Abstractions.Exceptions;

public class ForbiddenException : FamilySyncException
{
    public ForbiddenException()
    {
    }

    public ForbiddenException(string? message) : base(message)
    {
    }

    public ForbiddenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
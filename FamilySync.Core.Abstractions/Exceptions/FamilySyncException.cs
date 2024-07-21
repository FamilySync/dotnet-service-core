namespace FamilySync.Core.Abstractions.Exceptions;

public class FamilySyncException : Exception
{
    public FamilySyncException()
    {
    }

    public FamilySyncException(string? message) : base(message)
    {
    }

    public FamilySyncException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
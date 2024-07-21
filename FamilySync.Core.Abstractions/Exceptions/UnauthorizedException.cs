﻿namespace FamilySync.Core.Abstractions.Exceptions;

public class UnauthorizedException : FamilySyncException
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string? message) : base(message)
    {
    }

    public UnauthorizedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
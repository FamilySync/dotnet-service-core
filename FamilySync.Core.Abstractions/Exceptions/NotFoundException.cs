﻿namespace FamilySync.Core.Abstractions.Exceptions;

public class NotFoundException : ServiceException
{
    public NotFoundException()
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
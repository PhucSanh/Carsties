using System;

namespace Carsties.Shared.ExceptionHandler.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}


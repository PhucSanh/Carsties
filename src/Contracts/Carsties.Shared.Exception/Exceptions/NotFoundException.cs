using System;

namespace Carsties.Shared.ExceptionHandler.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }



}

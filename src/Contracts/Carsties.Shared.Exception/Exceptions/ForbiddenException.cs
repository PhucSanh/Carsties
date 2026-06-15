using System;
using System.Collections.Generic;
using System.Text;

namespace Carsties.Shared.ExceptionHandler.Exceptions
{
     public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}

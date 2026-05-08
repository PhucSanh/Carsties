using System;

namespace AuctionService;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}


using System;
using System.Linq;
using System.Threading.Tasks;
using Carsties.Shared.Data.DTOs.Auction;
using MassTransit;

namespace AuctionService.Consumers;

public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionDTO>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionDTO>> context)
    {
        Console.WriteLine("Received Fault for Auction Created Event: " + context.Message.Message.Make + " " + context.Message.Message.Model);
        Console.WriteLine("Exception Message: " + context.Message.Exceptions[0].Message);
        var exception = context.Message.Exceptions.First();
        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "DefaultModel";
            await context.Publish(context.Message.Message);
        }
        else
        {
            Console.WriteLine("Unhandled exception type: " + exception.ExceptionType);
            // Handle other types of exceptions if necessary
        }
    }
}
using AuctionService.Data;
using Carsties.Shared.Data.DTOs.Bid;
using MassTransit;

namespace AuctionService.Consumers
{
    public class BidPlacedConsumer : IConsumer<BidPlaced>
    {
        private readonly AuctionDBContext _dbContext;
        public BidPlacedConsumer(AuctionDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<BidPlaced> context)
        {
            Console.WriteLine("------>Consuming Bid Place--------------");

            var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);
            if((auction.CurrentHighestBid  == null ||
                context.Message.BidStatus.Contains("Accepted")) && context.Message.Amount>auction.CurrentHighestBid)
            {
                auction.CurrentHighestBid = context.Message.Amount;
                await _dbContext.SaveChangesAsync();

            }
        }
    }
}

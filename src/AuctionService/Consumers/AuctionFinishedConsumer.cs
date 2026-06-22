using AuctionService.Data;
using AuctionService.Entities.Enums;
using Carsties.Shared.Data.DTOs.Auction;
using MassTransit;

namespace AuctionService.Consumers
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinishedDTO>
    {
        private readonly AuctionDBContext _dbContext;
        public AuctionFinishedConsumer(AuctionDBContext auctionDBContext)
        {
            _dbContext = auctionDBContext;
            
        }
        public async Task Consume(ConsumeContext<AuctionFinishedDTO> context)
        {
            var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);
            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = context.Message.Amount;
            }
            auction.Status = auction.SoldAmount>auction.ReservePrice?Status.Ended: Status.ReserveNotMet;
            await _dbContext.SaveChangesAsync();

        }
    }
}

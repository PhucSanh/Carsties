using Carsties.Shared.Data.DTOs.Auction;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumer
{
    public class AuctionFinishedConsumer : IConsumer<AuctionFinishedDTO>
    {
        public async Task Consume(ConsumeContext<AuctionFinishedDTO> context)
        {
            var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
            if (context.Message.ItemSold)
            {
                auction.Winner = context.Message.Winner;
                auction.SoldAmount = (int)context.Message.Amount;

            }
            auction.Status = "Finished";
            await auction.SaveAsync();
        }
    }
}

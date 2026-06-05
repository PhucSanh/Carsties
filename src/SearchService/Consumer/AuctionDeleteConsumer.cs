using System;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumer;

public class AuctionDeleteConsumer : IConsumer<AuctionDeleteDTO>
{
    public async Task Consume(ConsumeContext<AuctionDeleteDTO> context)
    {
        await DB.DeleteAsync<Item>(context.Message.Id);
    }
}







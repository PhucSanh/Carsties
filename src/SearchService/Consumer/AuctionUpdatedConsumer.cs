using System;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumer;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdateDTO>
{
    private readonly IMapper _mapper;
    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;

    }

    public async Task Consume(ConsumeContext<AuctionUpdateDTO> context)
    {
        var item = await DB.Find<Item>().OneAsync(context.Message.Id);
        if (item == null)
        {
            Console.WriteLine("Item not found for update: " + context.Message.Id);
            return;
        }
        item = _mapper.Map(context.Message, item);
        await DB.SaveAsync(item);
    }
}

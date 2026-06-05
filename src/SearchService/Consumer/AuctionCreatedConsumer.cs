using System;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumer;

public class AuctionCreatedConsumer : IConsumer<AuctionDTO>
{
    private readonly IMapper _mapper;
    public AuctionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;

    }
    public async Task Consume(ConsumeContext<AuctionDTO> context)
    {
        Console.WriteLine("Received Auction Created Event: " + context.Message.Make + " " + context.Message.Model);
        var item = _mapper.Map<Item>(context.Message);

        await DB.SaveAsync(item);

    }
}

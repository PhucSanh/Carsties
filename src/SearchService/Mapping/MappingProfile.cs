using System;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using SearchService.Models;

namespace SearchService.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuctionDTO, Item>();
        CreateMap<AuctionUpdateDTO, Item>();
    }

}

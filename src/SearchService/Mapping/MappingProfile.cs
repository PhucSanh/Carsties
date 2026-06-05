using System;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using Carsties.Shared.Data.Entities;

namespace SearchService.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuctionDTO, Item>();

    }

}

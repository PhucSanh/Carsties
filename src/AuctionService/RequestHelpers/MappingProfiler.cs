using System;
using AuctionService.DTO;
using AuctionService.DTOs.Auction;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfiler : Profile
{
    public MappingProfiler()
    {
        CreateMap<Auction, AuctionDTO>().IncludeMembers(a => a.Item);
        CreateMap<Item, AuctionDTO>();
        CreateMap<AuctionCreateDTO, Auction>()
        .ForMember(dest => dest.Item, opt => opt.MapFrom(src => new Item
        {
            Make = src.Make,
            Model = src.Model,
            Year = src.Year,
            Color = src.Color,
            Mileage = src.Mileage
        }));

        CreateMap<AuctionUpdateDTO, Item>()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<AuctionUpdateDTO, Auction>()
                 .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src));

    }

}

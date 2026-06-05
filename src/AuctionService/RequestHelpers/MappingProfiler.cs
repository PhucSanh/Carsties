using System;

using AuctionService.Entities;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using Carsties.Shared.Data.Entities;

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
            Mileage = src.Mileage,
            ImageUrl = src.ImageUrl
        }));

        CreateMap<AuctionUpdateDTO, Item>()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<AuctionUpdateDTO, Auction>()
                 .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src));

        CreateMap<Auction, AuctionExportDTO>()
            .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Item.Make))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Item.Model))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Item.Year))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Item.Color))
            .ForMember(dest => dest.Mileage, opt => opt.MapFrom(src => src.Item.Mileage));




    }

}

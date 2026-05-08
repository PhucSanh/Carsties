using System;
using AuctionService.DTO;
using AuctionService.DTOs.Auction;

namespace AuctionService.Services.Auction;

public interface IAuctionService
{
    Task<AuctionDTO> CreateAuctionAsync(AuctionCreateDTO auctionDto);
    Task DeleteAuctionAsync(Guid id);
    Task<AuctionDTO> GetAuctionAsync(Guid id);
    Task<IEnumerable<AuctionDTO>> GetAuctionsAsync();
    Task UpdateAuctionAsync(Guid id, AuctionUpdateDTO auctionDto);
}

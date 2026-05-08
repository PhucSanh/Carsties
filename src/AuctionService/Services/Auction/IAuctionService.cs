using System;
using AuctionService.DTO;

namespace AuctionService.Services.Auction;

public interface IAuctionService
{
    Task<AuctionDTO> GetAuctionAsync(Guid id);
    Task<IEnumerable<AuctionDTO>> GetAuctionsAsync();
}

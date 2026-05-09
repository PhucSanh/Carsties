using System;
using AuctionService.Entities;
using AuctionService.Repositories.Generic;
using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.Data.Entities;


public interface IAuctionRepository : IGenericRepository<Auction>
{
    Task<IEnumerable<Auction>> GetAuctionsWithItemsAsync(AuctionRequestDTO requestDTO);

    Task<Auction> GetAuctionWithItemsAsync(Guid id);

}

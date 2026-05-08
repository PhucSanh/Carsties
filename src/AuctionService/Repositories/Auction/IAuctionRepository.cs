using System;
using AuctionService.Entities;
using AuctionService.Repositories.Generic;


public interface IAuctionRepository : IGenericRepository<Auction>
{
    Task<IEnumerable<Auction>> GetAuctionsWithItemsAsync();

    Task<Auction> GetAuctionWithItemsAsync(Guid id);

}

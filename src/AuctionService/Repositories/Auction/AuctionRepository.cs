using System;
using AuctionService.Data;
using AuctionService.Entities;
using AuctionService.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Repositories;

public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
{
    public AuctionRepository(AuctionDBContext auctionDBContext) : base(auctionDBContext)
    {
    }

    public async Task<IEnumerable<Auction>> GetAuctionsWithItemsAsync()
    {
        return await _auctionDBContext.Auctions.Include(a => a.Item).OrderBy(x => x.Item.Make).ToListAsync();
    }

    public async Task<Auction> GetAuctionWithItemsAsync(Guid id)
    {
        return await _auctionDBContext.Auctions.Include(a => a.Item).FirstOrDefaultAsync(a => a.Id == id);
    }
}


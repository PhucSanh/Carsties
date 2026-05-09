using System;
using AuctionService.Data;
using AuctionService.Repositories.Generic;
using Carsties.Shared.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Carsties.Shared.Data.DTOs.Request;

namespace AuctionService.Repositories;

public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
{
    public AuctionRepository(AuctionDBContext auctionDBContext) : base(auctionDBContext)
    {
    }

    public async Task<IEnumerable<Auction>> GetAuctionsWithItemsAsync(AuctionRequestDTO requestDTO)
    {
        if (requestDTO == null)
            return await _auctionDBContext.Auctions.Include(a => a.Item).OrderBy(x => x.Item.Make).ToListAsync();

        var query = _auctionDBContext.Auctions.Include(a => a.Item).OrderBy(x => x.Item.Make).AsQueryable();
        query = query.Where(a => a.Item.Make.Contains(requestDTO.SearchQuery ?? string.Empty) &&
                                 a.Item.Model.Contains(requestDTO.SearchQuery ?? string.Empty) &&
                                a.UpdatedAt.CompareTo(DateTime.Parse(requestDTO.DateTime ?? DateTime.MinValue.ToString())) > 0);
        return await query.ToListAsync();
    }

    public async Task<Auction> GetAuctionWithItemsAsync(Guid id)
    {
        return await _auctionDBContext.Auctions.Include(a => a.Item).FirstOrDefaultAsync(a => a.Id == id);
    }
}


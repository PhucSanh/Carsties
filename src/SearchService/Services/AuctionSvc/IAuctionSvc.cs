using System;
using SearchService.Models;

namespace SearchService.Services.AuctionSvc;

public interface IAuctionSvc
{
    public Task<List<Item>> GetItemForSearchAsync();
}

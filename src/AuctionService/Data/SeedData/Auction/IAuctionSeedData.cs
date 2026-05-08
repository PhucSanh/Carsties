using System;

namespace AuctionService.Data.SeedData.Auction;

public interface IAuctionSeedData
{
    Task SeedAuctionsAsync();

}

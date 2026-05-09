using System;
using AuctionService.Entities;
using Carsties.Shared.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDBContext : DbContext
{
    public AuctionDBContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionDBContext).Assembly);
    }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Item> Items { get; set; }


}

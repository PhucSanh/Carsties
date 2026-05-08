using System;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionService.Data.Configurations;

public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ReservePrice).IsRequired();
        builder.Property(a => a.Seller).IsRequired();
        builder.HasOne(a => a.Item)
            .WithOne(i => i.Auction)
            .HasForeignKey<Item>(i => i.AuctionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

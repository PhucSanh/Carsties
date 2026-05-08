using System;
using AuctionService.Data.SeedData.Auction;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class DBInitializer
{

    public static async Task Initialize(WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();

        var serviceProvider = scope.ServiceProvider;
        try
        {
            var context = serviceProvider.GetRequiredService<AuctionDBContext>();
            var auctionSeeder = serviceProvider.GetRequiredService<IAuctionSeedData>();
            await context.Database.MigrateAsync();

            if (await context.Auctions.AnyAsync())
            {
                Console.WriteLine("Database already seeded");
                return;
            }

            await auctionSeeder.SeedAuctionsAsync();
            Console.WriteLine("Database seeded successfully!");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred seeding the DB: {ex.Message}");
        }

    }


}

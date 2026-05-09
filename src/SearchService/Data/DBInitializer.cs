using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services.AuctionSvc;

namespace SearchService.Data;

public class DBInitializer
{
    public static async Task InitializeAsync(WebApplication web)
    {
        await DB.InitAsync("SearchService",
                MongoClientSettings.
                FromConnectionString(web.Configuration.GetConnectionString("DefaultConnection")));

        await DB.Index<Item>()
            .Key(i => i.Make, KeyType.Text)
            .Key(i => i.Model, KeyType.Text)
            .Key(i => i.Color, KeyType.Text)
            .Key(i => i.Seller, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        using var scope = web.Services.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<IAuctionSvc>();
        var items = await httpClient.GetItemForSearchAsync();
        Console.WriteLine($"Number of items in DB: {count}");
        if (items.Count > 0)
        {
            await DB.SaveAsync(items);
        }


    }

}

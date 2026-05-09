using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

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
            .CreateAsync();

        var count = await DB.CountAsync<Item>();
        if (count == 0)
        {
            var itemsData = await File.ReadAllTextAsync("Data/auctions.json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var items = JsonSerializer.Deserialize<List<Item>>(itemsData, options);
            if (items != null)
            {
                await DB.SaveAsync(items);
            }
        }

    }

}

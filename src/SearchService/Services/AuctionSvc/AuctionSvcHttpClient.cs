using System;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Repositories;

namespace SearchService.Services.AuctionSvc;

public class AuctionSvcHttpClient : IAuctionSvc
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ISearchRepository _searchRepository;
    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration configuration, ISearchRepository searchRepository)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _searchRepository = searchRepository;
    }

    public async Task<List<Item>> GetItemForSearchAsync()
    {
        var lastUpdate = await _searchRepository.GetLastUpdateTimeAsync();
        Console.WriteLine($"Last update time: {lastUpdate}");
        Console.WriteLine($"{_configuration["AuctionServiceUrl"]}/api/auctions?dateTime={lastUpdate}");
        return await _httpClient
        .GetFromJsonAsync<List<Item>>(
            $"{_configuration["AuctionServiceUrl"]}/api/auctions?dateTime={lastUpdate}");
    }

}

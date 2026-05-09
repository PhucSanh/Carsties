using System;
using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.Data.DTOs.Response;
using SearchService.Models;
using SearchService.Repositories;

namespace SearchService.Services.Search;

public class SearchServiceImpl : ISearchService
{
    private readonly ISearchRepository _searchRepository;
    public SearchServiceImpl(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }
    public async Task<ResponseDTO<List<Item>>> SearchAsync(RequestDTO requestDTO)
    {
        return await _searchRepository.SearchAsync(requestDTO);
    }
}

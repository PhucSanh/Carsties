using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.Data.DTOs.Response;
using Carsties.Shared.ExceptionHandler.Exceptions;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Repositories;

public class SearchRepository : ISearchRepository
{
    public async Task<string> GetLastUpdateTimeAsync()
    {
        var lastUpdate = await DB.Find<Item, string>()
        .Sort(x => x.Descending(x => x.UpdatedAt))
        .Project(x => x.UpdatedAt.ToString())
        .ExecuteFirstAsync();
        return lastUpdate;
    }

    public async Task<ResponseDTO<List<Item>>> SearchAsync(RequestDTO requestDTO)
    {
        var query = DB.PagedSearch<Item, Item>();
        if (!string.IsNullOrEmpty(requestDTO.SearchQuery))
        {
            query.Match(Search.Full, requestDTO.SearchQuery).SortByTextScore();
        }
        if (!string.IsNullOrEmpty(requestDTO.SortBy))
        {
            try
            {
                query = requestDTO.SortDirection?.ToLower() == "desc"
               ? query.Sort(x => x.Descending(requestDTO.SortBy))
               : query.Sort(x => x.Ascending(requestDTO.SortBy));
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Invalid sort field: {requestDTO.SortBy}");
            }

        }
        else if (string.IsNullOrEmpty(requestDTO.SearchQuery))
        {
            query.Sort(x => x.Ascending(a => a.Make));
        }

        query.PageNumber(requestDTO.PageNumber ?? 1);
        query.PageSize(requestDTO.PageSize ?? 10);

        var result = await query.ExecuteAsync();

        return new ResponseDTO<List<Item>>
        {
            Data = result.Results.ToList(),
            TotalCount = (int)result.TotalCount,
            PageCount = result.PageCount
        };
    }
}

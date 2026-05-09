using System;
using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.Data.DTOs.Response;
using SearchService.Models;

namespace SearchService.Services.Search;

public interface ISearchService
{
    public Task<ResponseDTO<List<Item>>> SearchAsync(RequestDTO requestDTO);
}

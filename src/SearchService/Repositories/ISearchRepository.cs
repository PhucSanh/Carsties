using System;
using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.Data.DTOs.Response;
using SearchService.Models;

namespace SearchService.Repositories;

public interface ISearchRepository
{
    public Task<ResponseDTO<List<Item>>> SearchAsync(RequestDTO requestDTO);

    public Task<string> GetLastUpdateTimeAsync();
}

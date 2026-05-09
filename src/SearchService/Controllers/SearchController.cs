using Carsties.Shared.Data.DTOs.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService.Models;
using SearchService.Services.Search;

namespace SearchService.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> Search([FromQuery] RequestDTO requestDTO)
        {
            var result = await _searchService.SearchAsync(requestDTO);
            return Ok(result);
        }
    }
}

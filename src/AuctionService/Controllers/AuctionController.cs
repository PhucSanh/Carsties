using AuctionService.DTO;
using AuctionService.Services.Auction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers
{
    [Route("api/auctions")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionDTO>>> GetAuctions()
        {
            var auctions = await _auctionService.GetAuctionsAsync();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuction(Guid id)
        {
            var auction = await _auctionService.GetAuctionAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            return Ok(auction);
        }
    }
}

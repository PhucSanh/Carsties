using AuctionService.DTO;
using AuctionService.DTOs.Auction;
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
            return Ok(auction);
        }

        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction(AuctionCreateDTO auctionDto)
        {
            var auction = await _auctionService.CreateAuctionAsync(auctionDto);
            return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, AuctionUpdateDTO auctionDto)
        {
            await _auctionService.UpdateAuctionAsync(id, auctionDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            await _auctionService.DeleteAuctionAsync(id);
            return NoContent();
        }
    }
}

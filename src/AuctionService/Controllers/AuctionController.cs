
using AuctionService.Services.Auction;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
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

        [HttpGet("export")]
        public async Task<ActionResult<byte[]>> ExportAuctionsToExcel()
        {
            var excelData = await _auctionService.ExportAuctionsToExcelAsync();
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Auctions.xlsx");
        }

        [HttpGet("export-template")]
        public ActionResult<byte[]> ExportTemplate()
        {
            var excelData = _auctionService.GenerateTemplateAsync();
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AuctionTemplate.xlsx");
        }

        [HttpPost("import")]
        public async Task<ActionResult> ImportAuctions([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new BadRequestException("No file uploaded.");
            }

            using var stream = file.OpenReadStream();
            var importDtos = await _auctionService.ImportAuctionsFromExcelAsync(stream);
            return CreatedAtAction(nameof(GetAuctions), null, importDtos);
        }
    }
}

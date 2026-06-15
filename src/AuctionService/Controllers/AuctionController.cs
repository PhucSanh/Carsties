
using AuctionService.Services.Auction;
using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.ExceptionHandler.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<IEnumerable<AuctionDTO>>> GetAuctions([FromQuery] AuctionRequestDTO requestDTO)
        {
            var auctions = await _auctionService.GetAuctionsAsync(requestDTO);
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDTO>> GetAuction(Guid id)
        {
            var auction = await _auctionService.GetAuctionAsync(id);
            return Ok(auction);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AuctionDTO>> CreateAuction(AuctionCreateDTO auctionDto)
        {
            var userName = User.Identity?.Name;
            var auction = await _auctionService.CreateAuctionAsync(auctionDto, userName);
            return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, auction);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, AuctionUpdateDTO auctionDto)
        {
            var userName = User.Identity?.Name;
            await _auctionService.UpdateAuctionAsync(id, auctionDto, userName);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
            var userName = User.Identity?.Name;
            await _auctionService.DeleteAuctionAsync(id, userName);
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

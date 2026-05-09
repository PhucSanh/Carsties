using System;
using Carsties.Shared.Data.DTOs.Auction;
using Carsties.Shared.Data.DTOs.Request;


namespace AuctionService.Services.Auction;

public interface IAuctionService
{
    Task<AuctionDTO> CreateAuctionAsync(AuctionCreateDTO auctionDto);
    Task DeleteAuctionAsync(Guid id);
    Task<AuctionDTO> GetAuctionAsync(Guid id);
    Task<IEnumerable<AuctionDTO>> GetAuctionsAsync(AuctionRequestDTO requestDTO);
    Task UpdateAuctionAsync(Guid id, AuctionUpdateDTO auctionDto);

    Task<byte[]> ExportAuctionsToExcelAsync();
    byte[] GenerateTemplateAsync();
    Task<bool> ImportAuctionsFromExcelAsync(Stream stream);
}

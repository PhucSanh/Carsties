using System;
using Carsties.Shared.Data.DTOs.Auction;
using Carsties.Shared.Data.DTOs.Request;


namespace AuctionService.Services.Auction;

public interface IAuctionService
{
    Task<AuctionDTO> CreateAuctionAsync(AuctionCreateDTO auctionDto, string seller);
    Task DeleteAuctionAsync(Guid id, string deleter);
    Task<AuctionDTO> GetAuctionAsync(Guid id);
    Task<IEnumerable<AuctionDTO>> GetAuctionsAsync(AuctionRequestDTO requestDTO);
    Task UpdateAuctionAsync(Guid id, AuctionUpdateDTO auctionDto, string updater);

    Task<byte[]> ExportAuctionsToExcelAsync();
    byte[] GenerateTemplateAsync();
    Task<bool> ImportAuctionsFromExcelAsync(Stream stream);
}

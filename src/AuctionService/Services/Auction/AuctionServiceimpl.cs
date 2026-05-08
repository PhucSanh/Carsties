using System;
using AuctionService.DTO;
using AuctionService.DTOs.Auction;
using AuctionService.Repositories;

using AutoMapper;
using Carsties.Shared.Excel.Service.Excel;

namespace AuctionService.Services.Auction;

public class AuctionServiceimpl : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IExcelService _excelService;
    private readonly IMapper _mapper;
    public AuctionServiceimpl(IAuctionRepository auctionRepository, IMapper mapper, IExcelService excelService)
    {
        _auctionRepository = auctionRepository;
        _mapper = mapper;
        _excelService = excelService;
    }

    public async Task<AuctionDTO> CreateAuctionAsync(AuctionCreateDTO auctionDto)
    {
        var auction = _mapper.Map<Entities.Auction>(auctionDto);
        auction.Id = Guid.NewGuid();
        auction.Seller = "John Doe";
        _auctionRepository.Add(auction);
        var result = await _auctionRepository.SaveChangesAsync();
        if (!result)
        {
            throw new BadRequestException("Failed to create auction.");
        }
        return _mapper.Map<AuctionDTO>(auction);
    }

    public async Task DeleteAuctionAsync(Guid id)
    {
        var auction = await _auctionRepository.GetByIdAsync(id);
        if (auction == null)
        {
            throw new NotFoundException($"Auction with id {id} not found.");
        }
        _auctionRepository.Delete(auction);
        var result = await _auctionRepository.SaveChangesAsync();
        if (!result)
        {
            throw new BadRequestException("Failed to delete auction.");
        }
    }

    public async Task<byte[]> ExportAuctionsToExcelAsync()
    {
        var auctions = await _auctionRepository.GetAuctionsWithItemsAsync();
        var exportDtos = _mapper.Map<IEnumerable<AuctionExportDTO>>(auctions);
        return _excelService.ExportToExcel(exportDtos, "Auctions");
    }

    public byte[] GenerateTemplateAsync()
    {
        return _excelService.GenerateTemplate<AuctionImportDTO>();
    }

    public async Task<AuctionDTO> GetAuctionAsync(Guid id)
    {
        var auction = await _auctionRepository.GetAuctionWithItemsAsync(id);
        if (auction == null)
        {
            throw new NotFoundException($"Auction with id {id} not found.");
        }
        return _mapper.Map<AuctionDTO>(auction);
    }

    public async Task<IEnumerable<AuctionDTO>> GetAuctionsAsync()
    {
        var auctions = await _auctionRepository.GetAuctionsWithItemsAsync();
        return _mapper.Map<IEnumerable<AuctionDTO>>(auctions);
    }

    public async Task<bool> ImportAuctionsFromExcelAsync(Stream stream)
    {
        var importResult = _excelService.ImportFromExcel<AuctionImportDTO>(stream, dto =>
        {
            if (dto.ReservePrice < 1000) return "Reserve price must be at least 1000";
            if (string.IsNullOrEmpty(dto.Make)) return "Make is required";
            return null;
        });
        if (importResult.Errors.Any())
        {
            var errorMessage = string.Join("; ", importResult.Errors);
            throw new BadRequestException($"Failed to import auctions from Excel. Errors: {errorMessage}");
        }

        var importDtos = importResult.Data;
        var auctions = _mapper.Map<IEnumerable<Entities.Auction>>(importDtos);
        foreach (var auction in auctions)
        {
            auction.Id = Guid.NewGuid();
            auction.Seller = "John Doe";
        }
        await _auctionRepository.AddRangeAsync(auctions);
        var result = await _auctionRepository.SaveChangesAsync();
        return result;
    }

    public async Task UpdateAuctionAsync(Guid id, AuctionUpdateDTO auctionDto)
    {
        var auction = await _auctionRepository.GetAuctionWithItemsAsync(id);
        if (auction == null)
        {
            throw new NotFoundException($"Auction with id {id} not found.");
        }
        _mapper.Map(auctionDto, auction);
        _auctionRepository.Update(auction);
        var result = await _auctionRepository.SaveChangesAsync();
        if (!result)
        {
            throw new BadRequestException("Failed to update auction.");
        }
    }
}

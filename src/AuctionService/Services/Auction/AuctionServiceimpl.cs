using System;


using AutoMapper;
using Carsties.Shared.Data.DTOs.Auction;
using Carsties.Shared.Data.DTOs.Request;
using Carsties.Shared.Excel.Service.Excel;
using Carsties.Shared.ExceptionHandler.Exceptions;
using MassTransit;

namespace AuctionService.Services.Auction;

public class AuctionServiceimpl : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IExcelService _excelService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    public AuctionServiceimpl(
        IAuctionRepository auctionRepository, IMapper mapper,
        IExcelService excelService, IPublishEndpoint publishEndpoint)
    {
        _auctionRepository = auctionRepository;
        _mapper = mapper;
        _excelService = excelService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<AuctionDTO> CreateAuctionAsync(AuctionCreateDTO auctionDto)
    {
        var auction = _mapper.Map<Carsties.Shared.Data.Entities.Auction>(auctionDto);
        auction.Id = Guid.NewGuid();
        auction.Seller = "John Doe";
        _auctionRepository.Add(auction);
        var result = await _auctionRepository.SaveChangesAsync();
        if (!result)
        {
            throw new BadRequestException("Failed to create auction.");
        }
        var actionDTO = _mapper.Map<AuctionDTO>(auction);
        await _publishEndpoint.Publish(actionDTO);
        return actionDTO;
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
        var auctions = await _auctionRepository.GetAuctionsWithItemsAsync(null);
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

    public async Task<IEnumerable<AuctionDTO>> GetAuctionsAsync(AuctionRequestDTO requestDTO)
    {
        var auctions = await _auctionRepository.GetAuctionsWithItemsAsync(requestDTO);
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
        var auctions = _mapper.Map<IEnumerable<Carsties.Shared.Data.Entities.Auction>>(importDtos);
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

using System;
using AuctionService.DTO;
using AuctionService.DTOs.Auction;
using AuctionService.Repositories;
using AutoMapper;

namespace AuctionService.Services.Auction;

public class AuctionServiceimpl : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IMapper _mapper;
    public AuctionServiceimpl(IAuctionRepository auctionRepository, IMapper mapper)
    {
        _auctionRepository = auctionRepository;
        _mapper = mapper;
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

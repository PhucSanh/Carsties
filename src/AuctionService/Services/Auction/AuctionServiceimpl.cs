using System;
using AuctionService.DTO;
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

    public async Task<AuctionDTO> GetAuctionAsync(Guid id)
    {
        var auction = await _auctionRepository.GetAuctionWithItemsAsync(id);
        return _mapper.Map<AuctionDTO>(auction);
    }

    public async Task<IEnumerable<AuctionDTO>> GetAuctionsAsync()
    {
        var auctions = await _auctionRepository.GetAuctionsWithItemsAsync();
        return _mapper.Map<IEnumerable<AuctionDTO>>(auctions);
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionService.DTOs.Auction;

public class AuctionCreateDTO
{
    [Required]
    public string Make { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    public int Mileage { get; set; }
    [Required]

    public string ImageUrl { get; set; }

    public int ReservePrice { get; set; }
    public DateTime AuctionEnd { get; set; }

}

using System;

namespace Carsties.Shared.Data.DTOs.Auction;

public class AuctionUpdateDTO
{
    public string Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }

}

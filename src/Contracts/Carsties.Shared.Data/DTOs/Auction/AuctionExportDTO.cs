using System;
using Carsties.Shared.Excel.Attributes.Excel;


namespace Carsties.Shared.Data.DTOs.Auction;

public class AuctionExportDTO
{
    [ExcelColumn("Reserve Price", Order = 1)]
    public int ReservePrice { get; set; }
    [ExcelColumn("Seller", Order = 2)]
    public string Seller { get; set; }

    [ExcelColumn("Winner", Order = 3)]
    public string Winner { get; set; }
    [ExcelColumn("Sold Amount", Order = 4)]
    public int SoldAmount { get; set; }
    [ExcelColumn("Current Highest Bid", Order = 5)]
    public int CurrentHighestBid { get; set; }
    [ExcelColumn("Auction End")]

    public DateTime AuctionEnd { get; set; }

    [ExcelColumn("Status", Order = 10)]
    public string Status { get; set; }
    [ExcelColumn("Make", Order = 11)]
    public string Make { get; set; }
    [ExcelColumn("Model", Order = 12)]
    public string Model { get; set; }
    [ExcelColumn("Year", Order = 13)]
    public int Year { get; set; }
    [ExcelColumn("Color", Order = 14)]
    public string Color { get; set; }
    [ExcelColumn("Mileage", Order = 15)]
    public int Mileage { get; set; }

}

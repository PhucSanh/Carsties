using System;
using Carsties.Shared.Excel.Attributes.Excel;

namespace Carsties.Shared.Data.DTOs.Auction;

public class AuctionImportDTO
{
    [ExcelColumn("Make", 1)]
    public string Make { get; set; }

    [ExcelColumn("Model", 2)]
    public string Model { get; set; }

    [ExcelColumn("Year", 3)]
    public int Year { get; set; }
    [ExcelColumn("Color", 4)]
    public string Color { get; set; }
    [ExcelColumn("Mileage", 5)]
    public int Mileage { get; set; }
    [ExcelColumn("Reserve Price", 6)]
    public int ReservePrice { get; set; }
    [ExcelColumn("Auction End", 7)]
    public DateTime AuctionEnd { get; set; }

}

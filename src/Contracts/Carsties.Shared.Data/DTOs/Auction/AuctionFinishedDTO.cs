using System;
using System.Collections.Generic;
using System.Text;

namespace Carsties.Shared.Data.DTOs.Auction
{
    public class AuctionFinishedDTO
    {
        public  bool ItemSold { get; set; }
        public string AuctionId { get; set; }
        public string Winner { get; set; }
        public string Seller { get; set; }

        public int? Amount { get; set; }

    }
}

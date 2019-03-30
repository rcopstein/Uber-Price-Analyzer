using System;

namespace Domain.Models
{
    public class Report
    {
        public float AveragePrice { get; set; }

        public float HighestPrice { get; set; }
        public DateTime HighestPriceDate { get; set; }

        public float LowestPrice { get; set; }
        public DateTime LowestPriceDate { get; set; }
    }
}

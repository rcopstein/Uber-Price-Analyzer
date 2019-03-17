using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class EstimateResponse
    {
        public IEnumerable<PriceReport> prices { get; set; }

        public EstimateResponse() {}
    }
}

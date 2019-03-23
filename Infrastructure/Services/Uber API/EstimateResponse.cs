using System.Collections.Generic;

namespace Infrastructure.Services.UberAPI
{
    public class EstimateResponse
    {
        public IEnumerable<Estimate> Prices { get; set; }
    }
}

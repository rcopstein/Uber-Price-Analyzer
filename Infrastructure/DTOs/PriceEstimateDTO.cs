using System;

namespace Infrastructure.DTOs
{
    public class PriceEstimateDTO
    {
        public Guid AnalysisId { get; set; }
        public virtual AnalysisDTO Analysis { get; set; }

        public DateTime Date { get; set; }
        public string ProductId { get; set; }
        public float LowEstimate { get; set; }
        public float HighEstimate { get; set; }
    }
}

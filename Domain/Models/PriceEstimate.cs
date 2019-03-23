using System;

namespace Domain.Models
{
    public class PriceEstimate
    {
        public DateTime Date { get; set; }
        public Guid AnalysisId { get; set; }
        public string ProductId { get; set; }
        public float LowEstimate { get; set; }
        public float HighEstimate { get; set; }

        public float AverageEstimate => (HighEstimate + LowEstimate) / 2;
    }
}

using System;
using Domain.Models;
using System.Collections.Generic;

namespace Infrastructure.DTOs
{
    public class AnalysisDTO
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }

        public TimeSpan Every { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public LocationDTO EndLocation { get; set; }
        public LocationDTO StartLocation { get; set; }

        public string Weekdays { get; set; }
        public virtual IEnumerable<PriceEstimateDTO> Prices { get; set; }
    }
}

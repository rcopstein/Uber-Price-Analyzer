using System;
using Domain.Models;

namespace Infrastructure.DTOs
{
    public class AnalysisDTO
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }

        public TimeSpan Every { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public LocationDTO EndLocation { get; set; }
        public LocationDTO StartLocation { get; set; }
    }
}

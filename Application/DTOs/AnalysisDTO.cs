using System;
using Domain.Models;

namespace Application.DTOs
{
    public class AnalysisDTO
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public TimeFrame TimeFrame { get; set; }
        public Location EndLocation { get; set; }
        public Location StartLocation { get; set; }
    }
}

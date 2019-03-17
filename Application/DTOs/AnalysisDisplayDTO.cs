using Domain.Models;

namespace Application.DTOs
{
    public class AnalysisDisplayDTO
    {
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
        public TimeFrame TimeFrame { get; set; }
        public Status Status { get; set; }
    }
}

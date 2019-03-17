using Domain.Models;

namespace Application.DTOs
{
    public class AnalysisIncludeDTO
    {
        public Location StartLocation { get; set; }
        public Location EndLocation { get; set; }
        public TimeFrame TimeFrame { get; set; }
    }
}

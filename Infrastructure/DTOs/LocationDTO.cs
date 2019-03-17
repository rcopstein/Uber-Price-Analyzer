using System;

namespace Infrastructure.DTOs
{
    public class LocationDTO
    {
        public Guid Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}

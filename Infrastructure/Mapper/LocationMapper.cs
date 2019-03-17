using Domain.Models;
using Infrastructure.DTOs;

namespace Infrastructure.Mapper
{
    public static class LocationMapper
    {
        public static Location FromDTO(LocationDTO dto)
        {
            if (dto == null) return null;
            return new Location(dto.Latitude, dto.Longitude);
        }

        public static LocationDTO ToDTO(Location entity)
        {
            if (entity == null) return null;

            return new LocationDTO()
            {
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };
        }
    }
}

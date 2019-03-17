using Domain.Models;

namespace Application.Validation.Specifications
{
    public class LocationWithinBounds : ISpecification<Location>
    {
        public bool Validate(Location property)
        {
            if (property == null) return false;

            if (property.Longitude < -180 || property.Longitude > 180) 
                return false;

            if (property.Latitude < -90 || property.Latitude > 90)
                return false;

            return true;
        }
    }
}

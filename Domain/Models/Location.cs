using Domain.Validation;

namespace Domain.Models
{
    public class Location : IValidatable
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public bool IsValid()
        {
            return Latitude >= -90 && Latitude <= 90 &&
                   Longitude >= -180 && Longitude <= 180;
        }

        public Location(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

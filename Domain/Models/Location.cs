using Domain.Validation;

namespace Domain.Models
{
    public class Location : IValidatable
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public ValidationSummary IsValid()
        {
            ValidationSummary summary = new ValidationSummary();

            if (Latitude < -90 || Latitude > 90)
                summary.Add(
                    "Latitude must be between -90 and 90", 
                    nameof(Latitude));

            if (Longitude < -180 || Longitude > 180)
                summary.Add(
                    "Longitude must be between -180 and 180",
                    nameof(Longitude));

            return summary;
        }

        public Location(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

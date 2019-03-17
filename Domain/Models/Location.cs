namespace Domain.Models
{
    public class Location
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Location(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

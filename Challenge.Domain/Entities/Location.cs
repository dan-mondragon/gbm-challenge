using System;

namespace Challenge.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

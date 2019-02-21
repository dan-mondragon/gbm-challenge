using System;
using System.Collections.Generic;

namespace Challenge.Domain.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Plate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Location> Locations { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

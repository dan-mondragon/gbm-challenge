using Challenge.Domain.Entities;
using System;

namespace Challenge.Domain.ViewModels
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int VehicleId { get; set; }
        public VehicleViewModel Vehicle { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

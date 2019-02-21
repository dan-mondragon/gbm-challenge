using System;

namespace Challenge.Domain.ViewModels
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }
        public string Plate { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class VehicleRequestViewModel
    {
        public string Plate { get; set; }
    }
}

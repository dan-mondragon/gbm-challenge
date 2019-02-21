
using System.ComponentModel.DataAnnotations;

namespace Challenge.Domain.ViewModels
{
    public class LocationRequestViewModel
    {
        [Required]
        [Range(-90,90)]
        public double? Latitude { get; set; }
        [Required]
        [Range(-180, 180)]
        public double? Longitude { get; set; }
        [Required]
        public int? VehicleId { get; set; }
    }
}

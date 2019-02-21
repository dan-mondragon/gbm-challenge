using Challenge.Domain.Entities;
using Challenge.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Domain.Converters
{
    public class VehicleConverter
    {
        /// <summary>
        /// Converts from Vehicle entity to VehicleViewModel
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public static VehicleViewModel Convert(Vehicle vehicle)
        {
            var vehicleViewModel = new VehicleViewModel();
            vehicleViewModel.Plate = vehicle.Plate;
            vehicleViewModel.VehicleId = vehicle.VehicleId;
            vehicleViewModel.UserId = vehicle.UserId;
            vehicleViewModel.CreatedAt = vehicle.CreatedAt;
            vehicleViewModel.UpdatedAt = vehicle.UpdatedAt;

            return vehicleViewModel;
        }

        /// <summary>
        /// Convert from Vehicle entities to VehicleViewModels
        /// </summary>
        /// <param name="vehicles"></param>
        /// <returns></returns>
        public static List<VehicleViewModel> ConvertList(IEnumerable<Vehicle> vehicles)
        {
            return vehicles.Select(v =>
            {
                var model = new VehicleViewModel();
                model.Plate = v.Plate;
                model.VehicleId = v.VehicleId;
                model.UserId = v.UserId;
                model.CreatedAt = v.CreatedAt;
                model.UpdatedAt = v.UpdatedAt;
                return model;
            }).ToList();
        }
    }
}

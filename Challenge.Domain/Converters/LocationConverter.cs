using Challenge.Domain.Entities;
using Challenge.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Domain.Converters
{
    public static class LocationConverter
    {
        /// <summary>
        /// Converts from Location entity to LocationViewModel
        /// </summary>
        /// <param name="location"></param>
        /// <returns>LocationViewModel</returns>
        public static LocationViewModel Convert(Location location)
        {
            if (location == null)
                return null;

            var locationViewModel = new LocationViewModel();
            locationViewModel.Latitude = location.Latitude;
            locationViewModel.Longitude = location.Longitude;
            locationViewModel.LocationId = location.LocationId;
            locationViewModel.Vehicle = location.Vehicle != null ? VehicleConverter.Convert(location.Vehicle) : null;
            locationViewModel.VehicleId = location.VehicleId;
            locationViewModel.CreatedAt = location.CreatedAt;

            return locationViewModel;
        }

        /// <summary>
        /// Convert from Location entities to LocationViewModels
        /// </summary>
        /// <param name="locations"></param>
        /// <returns>List<LocationViewModel></returns>
        public static List<LocationViewModel> ConvertList(IEnumerable<Location> locations)
        {
            return locations.Select(l =>
            {
                var model = new LocationViewModel();
                model.Latitude = l.Latitude;
                model.Longitude = l.Longitude;
                model.LocationId = l.LocationId;
                model.VehicleId = l.VehicleId;
                model.CreatedAt = l.CreatedAt;
                return model;
            }).ToList();
        }
    }
}

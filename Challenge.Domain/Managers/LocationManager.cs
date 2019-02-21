using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Domain.Converters;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Challenge.Domain.ViewModels;

namespace Challenge.Domain.Managers
{
    public class LocationManager : ILocationManager
    {
        public readonly ILocationRepository _repository;

        public LocationManager(ILocationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="locationViewModel"></param>
        /// <param name="ct"></param>
        /// <returns>LocationViewModel</returns>
        public async Task<LocationViewModel> AddLocationAsync(LocationRequestViewModel locationRequestViewModel, CancellationToken ct = default(CancellationToken))
        {
            var location = new Location
            {
                Longitude = (double)locationRequestViewModel.Longitude,
                Latitude = (double)locationRequestViewModel.Latitude,
                VehicleId = (int)locationRequestViewModel.VehicleId,
                CreatedAt = DateTime.Now
            };

            location = await _repository.AddAsync(location, ct);
            var locationViewModel = LocationConverter.Convert(location);
            return locationViewModel;
        }

        /// <summary>
        /// Get the historical location for a vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<LocationViewModel>> GetHistoricalLocationByVehicleIdAsync(int id, DateTime? startDate, DateTime? endDate, CancellationToken ct = default(CancellationToken))
        {
            var locations = await _repository.GetHistoricalByVehicleIdAsync(id, startDate, endDate, ct);
            return LocationConverter.ConvertList(locations).ToList();
        }

        /// <summary>
        /// Get the current location for a vehicle.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<LocationViewModel> GetLocationByVehicleIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var location = await _repository.GetCurrentByVehicleIdAsync(id, ct);
            return LocationConverter.Convert(location);
        }
    }
}

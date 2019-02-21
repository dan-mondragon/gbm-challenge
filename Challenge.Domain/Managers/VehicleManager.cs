using Challenge.Domain.Converters;
using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Challenge.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Managers
{
    public class VehicleManager : IVehicleManager
    {
        public readonly IVehicleRepository _repository;

        public VehicleManager(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Creates new vehicle
        /// </summary>
        /// <param name="vehicleViewModel"></param>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<VehicleViewModel> AddVehicleAsync(VehicleRequestViewModel vehicleViewModel, string userId, CancellationToken ct = default(CancellationToken))
        {
            var vehicle = new Vehicle
            {
                Plate = vehicleViewModel.Plate,
                UserId = int.Parse(userId),
                CreatedAt = DateTime.Now
            };

            vehicle = await _repository.AddAsync(vehicle, ct);
            return VehicleConverter.Convert(vehicle);
        }

        /// <summary>
        /// Deletes existing vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVehicleAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _repository.DeleteAsync(id, ct);
        }

        /// <summary>
        /// List all vehicles
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<VehicleViewModel>> GetAllVehicleAsync(CancellationToken ct = default(CancellationToken))
        {
            var vehicles = VehicleConverter.ConvertList(await _repository.GetAllAsync(ct));
            return vehicles.ToList();
        }

        /// <summary>
        /// Get a vehicle by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<VehicleViewModel> GetVehicleByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            var vehicleViewModel = VehicleConverter.Convert(await _repository.GetByIdAsync(id, ct));
            return vehicleViewModel;
        }

        /// <summary>
        /// Updates a vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vehicleViewModel"></param>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> UpdateVehicleAsync(int id, VehicleRequestViewModel vehicleViewModel, int userId, CancellationToken ct = default(CancellationToken))
        {
            var vehicle = await _repository.GetByIdAsync(id, ct);

            if (vehicle == null) return false;
            vehicle.Plate = vehicleViewModel.Plate;
            vehicle.UserId = userId;
            vehicle.UpdatedAt = DateTime.Now;

            return await _repository.UpdateAsync(vehicle, ct);
        }

        /// <summary>
        /// Check if vehicle belongs to user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> isValidUserVehicle(int userId, int id, CancellationToken ct = default(CancellationToken))
        {
            var vehicleIds = await _repository.GetVehicesByUser(userId, ct);
            if (vehicleIds.Contains(id))
                return true;

            return false;
        }
    }
}

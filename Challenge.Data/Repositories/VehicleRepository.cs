using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ChallengeContext _context;

        public VehicleRepository(ChallengeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check if vehicle exists
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task<bool> VehicleExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Vehicle>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Vehicle.ToListAsync(ct);
        }

        public async Task<Vehicle> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Vehicle.FindAsync(id);
        }

        /// <summary>
        /// Creates a new vehicle
        /// </summary>
        /// <param name="newVehicle"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Vehicle> AddAsync(Vehicle newVehicle, CancellationToken ct = default(CancellationToken))
        {
            _context.Vehicle.Add(newVehicle);
            await _context.SaveChangesAsync(ct);
            return newVehicle;
        }

        /// <summary>
        /// Updates an existing vehicle
        /// </summary>
        /// <param name="Vehicle"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Vehicle Vehicle, CancellationToken ct = default(CancellationToken))
        {
            if (!await VehicleExists(Vehicle.VehicleId, ct))
                return false;
            _context.Vehicle.Update(Vehicle);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        /// <summary>
        /// Deletes an existing vehicle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await VehicleExists(id, ct))
                return false;
            var toRemove = _context.Vehicle.Find(id);
            _context.Vehicle.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        /// <summary>
        /// Get all the vehicles that belong to user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<int>> GetVehicesByUser(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Vehicle.Where(v => v.UserId == id).Select(v => v.VehicleId).ToListAsync();
        }
    }
}

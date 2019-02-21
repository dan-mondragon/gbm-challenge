using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Data.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ChallengeContext _context;

        public LocationRepository(ChallengeContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Creates a new Location record
        /// </summary>
        /// <param name="newLocation"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Location> AddAsync(Location newLocation, CancellationToken ct = default(CancellationToken))
        {
            _context.Location.Add(newLocation);
            await _context.SaveChangesAsync(ct);
            return newLocation;
        }

        /// <summary>
        /// Get all the location records for a vehicle.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<List<Location>> GetHistoricalByVehicleIdAsync(int id, DateTime? startDate, DateTime? endDate, CancellationToken ct = default(CancellationToken))
        {
            startDate = startDate != null ? startDate : DateTime.MinValue;
            endDate = endDate != null ? endDate : DateTime.MaxValue;

            return await _context.Location.Where(l => l.VehicleId == id && l.CreatedAt.Date >= startDate.Value.Date && l.CreatedAt.Date <= endDate.Value.Date).ToListAsync(ct);
        }


        /// <summary>
        /// Get the current location for a vehicle.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<Location> GetCurrentByVehicleIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Location.Where(l => l.VehicleId == id).Include(l => l.Vehicle).OrderByDescending(l => l.CreatedAt).FirstOrDefaultAsync(ct);
        }
    }
}

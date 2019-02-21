using Challenge.Domain.Entities;
using Challenge.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.UnitTest.MockData
{
    public class LocationRepository : ILocationRepository
    {
        private readonly List<Location> _locations;
        public LocationRepository()
        {
            _locations = new List<Location>()
            {
                new Location(){ Latitude = 19.433410, Longitude = -99.196537, VehicleId = 1, LocationId = 1,  CreatedAt = DateTime.Now},
                new Location(){ Latitude = 19.123456, Longitude = -99.000000, VehicleId = 2, LocationId = 2,  CreatedAt = DateTime.Now},
                new Location(){ Latitude = 19.963258, Longitude = -99.123456, VehicleId = 3, LocationId = 3,  CreatedAt = DateTime.Now},
                new Location(){ Latitude = 19.741258, Longitude = -99.987654, VehicleId = 3, LocationId = 4,  CreatedAt = DateTime.Now}
            };
        }

        public async Task<Location> AddAsync(Location Location, CancellationToken ct = default(CancellationToken))
        {
            Location.LocationId = _locations.Count + 1;
            _locations.Add(Location);
            return Location;
        }

        public void Dispose()
        {
            
        }

        public async Task<Location> GetCurrentByVehicleIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return _locations.Where(l => l.VehicleId == id).OrderByDescending(l => l.CreatedAt).FirstOrDefault();
        }

        public async Task<List<Location>> GetHistoricalByVehicleIdAsync(int id, DateTime? startDate, DateTime? endDate, CancellationToken ct = default(CancellationToken))
        {
            return _locations.Where(l => l.VehicleId == id).ToList();
        }
    }
}

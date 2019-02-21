using Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Repositories
{
    public interface ILocationRepository : IDisposable
    {
        Task<List<Location>> GetHistoricalByVehicleIdAsync(int id, DateTime? startDate, DateTime? endDate, CancellationToken ct = default(CancellationToken));
        Task<Location> GetCurrentByVehicleIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Location> AddAsync(Location Location, CancellationToken ct = default(CancellationToken));
    }
}

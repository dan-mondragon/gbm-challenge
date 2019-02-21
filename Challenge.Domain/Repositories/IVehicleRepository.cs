using Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Repositories
{
    public interface IVehicleRepository : IDisposable
    {
        Task<List<Vehicle>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Vehicle> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Vehicle> AddAsync(Vehicle Vehicle, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Vehicle Vehicle, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<int>> GetVehicesByUser(int id, CancellationToken ct = default(CancellationToken));
    }
}

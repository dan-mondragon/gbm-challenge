using Challenge.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Managers
{
    public interface IVehicleManager
    {
        Task<List<VehicleViewModel>> GetAllVehicleAsync(CancellationToken ct = default(CancellationToken));
        Task<VehicleViewModel> GetVehicleByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<VehicleViewModel> AddVehicleAsync(VehicleRequestViewModel VehicleViewModel, string userId, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateVehicleAsync(int id, VehicleRequestViewModel vehicleViewModel, int userId, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteVehicleAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<bool> isValidUserVehicle(int userId, int id, CancellationToken ct = default(CancellationToken));
    }
}

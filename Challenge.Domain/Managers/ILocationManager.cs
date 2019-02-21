using Challenge.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Domain.Managers
{
    public interface ILocationManager
    {
        Task<LocationViewModel> AddLocationAsync(LocationRequestViewModel locationRequestViewModel, CancellationToken ct = default(CancellationToken));
        Task<List<LocationViewModel>> GetHistoricalLocationByVehicleIdAsync(int id, DateTime? startDate, DateTime? endDate, CancellationToken ct = default(CancellationToken));
        Task<LocationViewModel> GetLocationByVehicleIdAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}

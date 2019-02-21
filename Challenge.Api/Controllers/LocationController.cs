using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Domain.Managers;
using Challenge.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [Authorize]
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationManager _manager;
        private readonly IVehicleManager _vehicleManager;

        public LocationController(ILocationManager manager, IVehicleManager vehicleManager)
        {
            _manager = manager;
            _vehicleManager = vehicleManager;
        }

        [HttpPost]
        [Produces(typeof(LocationViewModel))]
        public async Task<IActionResult> Post([FromBody]LocationRequestViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();

                //For testing
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = int.Parse(((ClaimsIdentity)User.Identity).Name);
                if (!await _vehicleManager.isValidUserVehicle(userId, (int)input.VehicleId, ct))
                {
                    return Forbid();
                }

                return StatusCode(201, await _manager.AddLocationAsync(input, ct));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = "ServerError",
                    ex.Message
                });
            }
        }

        [HttpGet("vehicle/{id}/historical")]
        [Produces(typeof(List<LocationViewModel>))]
        public async Task<IActionResult> GetHistorical(int id, [FromQuery] DateTime? StartDate, [FromQuery] DateTime? EndDate, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var userId = int.Parse(((ClaimsIdentity)User.Identity).Name);
                if(!await _vehicleManager.isValidUserVehicle(userId, id, ct))
                {
                    return Forbid();
                }

                var vehicle = await _manager.GetHistoricalLocationByVehicleIdAsync(id, StartDate, EndDate, ct);
                if (vehicle == null)
                {
                    return NotFound();
                }

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = "ServerError",
                    ex.Message
                });
            }
        }

        [HttpGet("vehicle/{id}/current")]
        [Produces(typeof(List<LocationViewModel>))]
        public async Task<IActionResult> GetCurrent(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var userId = int.Parse(((ClaimsIdentity)User.Identity).Name);
                if (!await _vehicleManager.isValidUserVehicle(userId, id, ct))
                {
                    return Forbid();
                }

                var vehicle = await _manager.GetLocationByVehicleIdAsync(id, ct);
                if (vehicle == null)
                {
                    return NotFound();
                }

                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Code = "ServerError",
                    ex.Message
                });
            }
        }
    }
}
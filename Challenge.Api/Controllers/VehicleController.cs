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
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleManager _manager;

        public VehicleController(IVehicleManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Produces(typeof(List<VehicleViewModel>))]
        public async Task<IActionResult> Get(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var userId = int.Parse(User.Identity.Name);
                if (userId != 1)
                {
                    return Forbid();
                }

                return new ObjectResult(await _manager.GetAllVehicleAsync(ct));
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

        [HttpGet("{id}")]
        [Produces(typeof(VehicleViewModel))]
        public async Task<IActionResult> Get(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var userId = int.Parse(User.Identity.Name);
                if (userId != 1 && !await _manager.isValidUserVehicle(userId, id, ct))
                {
                    return Forbid();
                }

                var vehicle = await _manager.GetVehicleByIdAsync(id, ct);
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

        [HttpPost]
        [Produces(typeof(VehicleViewModel))]
        public async Task<IActionResult> Post([FromBody]VehicleRequestViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();

                var userId = ((ClaimsIdentity)User.Identity).Name;

                return StatusCode(201, await _manager.AddVehicleAsync(input, userId, ct));
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

        [HttpPut("{id}")]
        [Produces(typeof(VehicleViewModel))]
        public async Task<IActionResult> Put(int id, [FromBody]VehicleRequestViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();

                var userId = int.Parse(User.Identity.Name);
                if (!await _manager.isValidUserVehicle(userId, id, ct))
                {
                    return Forbid();
                }

                if (await _manager.GetVehicleByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }
                
                if (await _manager.UpdateVehicleAsync(id, input, userId, ct))
                {
                    return Ok(input);
                }

                return StatusCode(500);
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

        [HttpDelete("{id}")]
        [Produces(typeof(void))]
        public async Task<ActionResult> Delete(int id, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var userId = int.Parse(User.Identity.Name);
                if (!await _manager.isValidUserVehicle(userId, id, ct))
                {
                    return Forbid();
                }

                if (await _manager.GetVehicleByIdAsync(id, ct) == null)
                {
                    return NotFound();
                }

                if (await _manager.DeleteVehicleAsync(id, ct))
                {
                    return Ok();
                }

                return StatusCode(500);
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

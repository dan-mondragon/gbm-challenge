using System;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Domain.Exceptions;
using Challenge.Domain.Managers;
using Challenge.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _manager;

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpPost("register")]
        [Produces(typeof(UserViewModel))]
        public async Task<IActionResult> Register([FromBody]UserRequestViewModel input, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, await _manager.AddUserAsync(input, ct));
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
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

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserRequestViewModel user)
        {
            try
            {
                var result = await _manager.Authenticate(user.Username, user.Password);

                if (result == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(result);
            }
            catch(Exception ex)
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
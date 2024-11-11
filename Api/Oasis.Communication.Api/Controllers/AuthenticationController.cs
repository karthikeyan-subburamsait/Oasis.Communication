using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oasis.Communication.Api.Models;
using Oasis.Communication.Api.Services;

namespace Oasis.Communication.Api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = nameof(Roles.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] User user)
        {
            string accessToken = string.Empty;

            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Invalid UserName/Password");
            }

            try
            {
                bool userExists = await _authenticationService.HasUserExists(user.Name, user.Password);

                if (userExists)
                {
                    accessToken = await _authenticationService.GetAccessToken(user.Name, user.Password);
                }
                else
                {
                    return NotFound("User Not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Sothing went wrong");
            }

            return Ok(accessToken);
        }

        [Route("ValidateToken")]
        [HttpPost]
        public async Task<IActionResult> ValidateToken([FromBody] User user)
        {
            return Ok();
        }
    }
}

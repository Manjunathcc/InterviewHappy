using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.services;
using StudentManagement.Domain.Models;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Logs in a user using the provided credentials.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>
        ///   <list type="bullet">
        ///     <item><description>200 OK: If login is successful, returns a success message.</description></item>
        ///     <item><description>400 Bad Request: If the payload is invalid or login fails, returns an error message.</description></item>
        ///     <item><description>500 Internal Server Error: If an unexpected error occurs during login, returns an error message.</description></item>
        ///   </list>
        /// </returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="model">The registration details.</param>
        /// <returns>
        ///   <list type="bullet">
        ///     <item><description>201 Created: If registration is successful, returns the newly created user details.</description></item>
        ///     <item><description>400 Bad Request: If the payload is invalid or registration fails, returns an error message.</description></item>
        ///     <item><description>500 Internal Server Error: If an unexpected error occurs during registration, returns an error message.</description></item>
        ///   </list>
        /// </returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Registeration(model, UserRoles.User);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

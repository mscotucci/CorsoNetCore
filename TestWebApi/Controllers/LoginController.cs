using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application;
using TestWebApi.Exceptions;
using TestWebApi.Requests.Login;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = await _loginService.LoginAsync(loginRequest);
                return Ok(token);
            }
            catch (InvalidCredentialException)
            {
                return Unauthorized();
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application;
using TestWebApi.Requests.Users;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRequest createUserRequest)
        {
            var response  = await _usersService.CreateUserAsync(createUserRequest);
            return Ok(response);
        }
    }
}

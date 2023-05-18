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
            var response = await _usersService.CreateUserAsync(createUserRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> SearchUsersAsync(SearchUserRequest searchUserRequest)
        {

            var response = await _usersService.SearchUsersAsync(searchUserRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(UpdateUserRequest updateUserRequest)
        {

            await _usersService.UpdateUserAsync(updateUserRequest);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {

            var response = await _usersService.ReadUserAsync(id);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {

           await _usersService.DeleteUserAsync(id);
            return NoContent();
        }


    }
}

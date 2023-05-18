using BookStoreApi.Requests.Books;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application;
using TestWebApi.Application.Impl;
using TestWebApi.Responses.Books;
using TestWebApi.Responses;
using TestWebApi.Requests.Roles;
using TestWebApi.Responses.Roles;
using Microsoft.AspNetCore.Authorization;
using TestWebApi.Requests.Books;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _rolesService;

        public RolesController(IRoleService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> SearchRole([FromQuery] SearchRoleRequest searchRoleRequest)
        {
            PagedResultsResponse<RoleModelResponse> rolesSearchResults = await _rolesService.SearchRolesAsync(searchRoleRequest);

            return Ok(rolesSearchResults);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleRole(int id)
        {
            RoleModelResponse? roleResponse = await _rolesService.ReadRoleAsync(id);
            if (roleResponse == null)
            {
                return NotFound();
            }
            return Ok(roleResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest createRoleRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _rolesService.CreateRoleAsync(createRoleRequest);
            return Ok(response);
        }
    }
}

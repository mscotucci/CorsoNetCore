using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application;
using TestWebApi.Exceptions;
using TestWebApi.Infrastructure.SearchCriteria;
using TestWebApi.Requests.Authors;
using TestWebApi.Responses;
using TestWebApi.Responses.Authors;

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAuthors([FromQuery] SearchAuthorRequest searchAuthorRequest)
        {
            PagedResultsResponse<AuthorModelResponse> authorsSearchResults = await _authorsService.SearchAuthorsAsync(searchAuthorRequest);
            return Ok(authorsSearchResults);
        }

        [HttpPost]

        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest createAuthorRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _authorsService.CreateAuthorAsync(createAuthorRequest);
            return Ok(response);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorRequest updateAuthorRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _authorsService.UpdateAuthorAsync(updateAuthorRequest);
                return NoContent();
            }
            catch (AuthorNotFoundException)
            {

                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorsService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}

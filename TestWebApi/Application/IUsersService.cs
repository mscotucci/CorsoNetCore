using TestWebApi.Requests.Users;
using TestWebApi.Responses;
using TestWebApi.Responses.Users;

namespace TestWebApi.Application
{
    public interface IUsersService
    {
        Task<UserModelResponse> CreateUserAsync(CreateUserRequest createUserRequest);
        Task DeleteUserAsync(string userId);
        Task<UserModelResponse?> ReadUserAsync(string id);
        Task<PagedResultsResponse<UserModelResponse>> SearchUsersAsync(SearchUserRequest searchUserRequest);
        Task UpdateUserAsync(UpdateUserRequest updateUserRequest);
    }
}

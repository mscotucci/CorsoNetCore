using TestWebApi.Requests.Roles;
using TestWebApi.Responses.Roles;
using TestWebApi.Responses;

namespace TestWebApi.Application
{
    public interface IRoleService
    {
        Task<RoleModelResponse> CreateRoleAsync(CreateRoleRequest createRoleRequest);
        Task DeleteRoleAsync(int roleId);
        Task<RoleModelResponse?> ReadRoleAsync(int id);
        Task<PagedResultsResponse<RoleModelResponse>> SearchRolesAsync(SearchRoleRequest searchRoleRequest);
        Task UpdateRoleAsync(UpdateRoleRequest updateRoleRequest);
    }
}

using Microsoft.EntityFrameworkCore;
using TestWebApi.Entities;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Authors;
using TestWebApi.Requests.Roles;
using TestWebApi.Responses;
using TestWebApi.Responses.Authors;
using TestWebApi.Responses.Roles;
using TestWebApi.Responses.Users;

namespace TestWebApi.Application.Impl
{
    public class RolesService : IRoleService
    {
        private readonly BookStoreDbContext _dbContext;

        public RolesService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RoleModelResponse> CreateRoleAsync(CreateRoleRequest createRoleRequest)
        {
            var role = new Role()
            {
                Name = createRoleRequest.Name
            };
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            return new RoleModelResponse { Id = role.Id, Name = role.Name };
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == roleId);

            if (role == null)
            {
                throw new Exception("ruolo non trovato");
            }
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<RoleModelResponse?> ReadRoleAsync(int id)
        {
            var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == id);
            if (role != null)
            {
                var response = new RoleModelResponse
                {
                    Name = role.Name,
                    Id = id,
                };
                return response;
            }
            return null;
        }

        public async Task<PagedResultsResponse<RoleModelResponse>> SearchRolesAsync(SearchRoleRequest searchRoleRequest)
        {
            var query = _dbContext.Roles.AsQueryable();
            if (searchRoleRequest.Name != null)
            {
                query = query.Where(x => x.Name.Contains(searchRoleRequest.Name));
            }
            int totalCount = await query.CountAsync();

            var results = query.Select(role => new RoleModelResponse
            {
                Id = role.Id,
                Name = role.Name,
            });

            PagedResultsResponse<RoleModelResponse> searchResults = await PagedResultsResponse<RoleModelResponse>.CreateAsync(
                searchRoleRequest.Page,
                searchRoleRequest.PageSize,
                totalCount,
                results
            );
            return searchResults;
        }

        public async Task UpdateRoleAsync(UpdateRoleRequest updateRoleRequest)
        {
            var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == updateRoleRequest.Id);

            if (role == null)
            {
                throw new Exception("nessun ruolo trovato");
            }

            role.Name = updateRoleRequest.Name ?? role.Name;
            await _dbContext.SaveChangesAsync();
            
        }
    }
}

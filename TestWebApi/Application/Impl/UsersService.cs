using Microsoft.EntityFrameworkCore;
using TestWebApi.Entities;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Users;
using TestWebApi.Responses;
using TestWebApi.Responses.Users;

namespace TestWebApi.Application.Impl
{
    public class UsersService : IUsersService
    {
        private readonly BookStoreDbContext _dbContext;

        public UsersService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserModelResponse> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            User user = new User();

            user.Id = Guid.NewGuid();
            user.Username = createUserRequest.Username;

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var response = new UserModelResponse { Id = user.Id.ToString(), Username = user.Username};
            return response;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id.ToString() == userId);
            if(user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<UserModelResponse?> ReadUserAsync(string id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id.ToString() == id);
            if (user != null)
            {
                var response = new UserModelResponse
                {
                    Username = user.Username,
                    Id = id,
                };
                return response;
            }
            return null;

        }

        public async Task<PagedResultsResponse<UserModelResponse>> SearchUsersAsync(SearchUserRequest searchUserRequest)
        {
            IQueryable<User> query = _dbContext.Users.AsQueryable();
            if(searchUserRequest.Username != null) {
                query = query.Where(x => x.Username.Contains(searchUserRequest.Username));
            }
            int totalCount = await query.CountAsync();
            IQueryable<UserModelResponse> select = query.Select(x => new UserModelResponse { Id = x.Id.ToString(), Username = x.Username});
            PagedResultsResponse<UserModelResponse> response = await PagedResultsResponse<UserModelResponse>.CreateAsync(
                searchUserRequest.Page,
                searchUserRequest.PageSize,
                totalCount,
                select
                );

            return response;

        }

        public async Task UpdateUserAsync(UpdateUserRequest updateUserRequest)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.ToString() == updateUserRequest.Id);
            if(user != null)
            {
                //Da completare quando aggiungeremo i campi
            }
        }
    }
}

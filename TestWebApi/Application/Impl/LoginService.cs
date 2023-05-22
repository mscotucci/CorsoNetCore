using Microsoft.EntityFrameworkCore;
using TestWebApi.Exceptions;
using TestWebApi.Infrastructure;
using TestWebApi.Requests.Login;

namespace TestWebApi.Application.Impl;

public class LoginService : ILoginService
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IJwtService _jwtService;

    public LoginService(BookStoreDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    public async Task<string> LoginAsync(LoginRequest loginRequest)
    {
       //Recuperiamo lo user dal db
       var user = await _dbContext.Users.Include(x=>x.Roles).SingleOrDefaultAsync(x=>x.Username == loginRequest.Username);
        if (user == null)
        {
            throw new InvalidCredentialException();
        }
        //Creare il token
        var token = await _jwtService.GenerateAsync(user);

        //restituiamo il token
        return token;
    }
}

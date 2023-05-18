using TestWebApi.Entities;

namespace TestWebApi.Application;

public interface IJwtService
{
    Task<string> GenerateAsync(User user);
}

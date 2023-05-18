using TestWebApi.Requests.Login;

namespace TestWebApi.Application
{
    public interface ILoginService
    {
        Task<string> LoginAsync(LoginRequest loginRequest);
    }
}

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestWebApi.Entities;

namespace TestWebApi.Application.Impl;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string> GenerateAsync(User user)
    {

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name,user.Username)
        };

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value)), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration.GetSection("Jwt:Issuer").Value,
            _configuration.GetSection("Jwt:Audience").Value,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
            );
        string stringToken = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(stringToken);
    }
}

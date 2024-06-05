using JwtTest.Interfaces;
using JwtTest.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace JwtTest.Services;

public class LoginService : ILoginService
{
    readonly IConfiguration configuration;
    readonly ITokenService tokenService;
    private readonly IDistributedCache _cache;

    public LoginService(IConfiguration configuration, ITokenService tokenService, IDistributedCache cache)
    {
        this.configuration = configuration;
        this.tokenService = tokenService;
        _cache = cache;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var userJson = _cache.GetString(request.Username);
        if(userJson == null)
            throw new Exception("Unauthorized");
        User? user = JsonSerializer.Deserialize<User>(userJson);

        if(user == null || user.password != request.Password)
            throw new Exception("Unauthorized");

        var token = await tokenService.GenerateToken(request.Username);
            return new LoginResponse {
                    Username = user.username,
                    Name= user.name,
                    Token = token.Token
                };

    }
}

using JwtTest.Interfaces;
using JwtTest.Models;

namespace JwtTest.Services;

public class LoginService : ILoginService
{
    readonly IConfiguration configuration;
    readonly ITokenService tokenService;

    public LoginService(IConfiguration configuration, ITokenService tokenService)
    {
        this.configuration = configuration;
        this.tokenService = tokenService;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {

        if(request.Username == "admin" && request.Password == "admin") {
            var token = await tokenService.GenerateToken(request.Username);
            return new LoginResponse {
                    Username = "admin",
                    Token = token.Token
                };
            }
            else
                throw new Exception("Unauthorized");


    }
}

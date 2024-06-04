using JwtTest.Models;

namespace JwtTest.Interfaces {

    public interface ITokenService
    {
        public Task<TokenResponse> GenerateToken(string username);
    }
}

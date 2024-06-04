using JwtTest.Models;

namespace JwtTest.Interfaces {

    public interface ILoginService
    {
        public Task<LoginResponse> Login(LoginRequest request);
    }
}

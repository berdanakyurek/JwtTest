using Microsoft.AspNetCore.Mvc;
using JwtTest.Models;
using JwtTest.Interfaces;


namespace JwtTest.Controllers.Login {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService loginService;

        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            this.loginService = loginService;
        }

        [HttpPost(Name = "Login")]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return  await loginService.Login(request);
        }
    }
}

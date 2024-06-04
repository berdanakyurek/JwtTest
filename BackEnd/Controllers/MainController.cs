using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace JwtTest.Controllers.MainApi {

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MainApiController : ControllerBase
    {

        private readonly ILogger<MainApiController> _logger;

        public MainApiController(ILogger<MainApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "AdminEndpoint")]
        [Authorize]
        public String AdminEndpoint()
        {
            return "Admin Content";
        }

        [HttpGet(Name = "PublicEndpoint")]
        public String PublicEndpoint()
        {
            return "Public Content";
        }
    }
}

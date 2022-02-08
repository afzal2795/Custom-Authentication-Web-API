using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Food_Order_Custom_Authentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;
        public AuthenticationController(ICustomAuthenticationManager customAuthenticationManager)
        {
            _customAuthenticationManager = customAuthenticationManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "New Jersey", "New York", "Texas", "Maxico" };
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Login(string email, string password)
        {
            var token = _customAuthenticationManager.Authenticate(email, password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost("Unauthenticate")]
        public IActionResult Logout()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            var status = _customAuthenticationManager.Unauthenticate(authorizationHeader);
            if (status)
                return Ok("Logged Out");
            return NotFound();
        }
    }
}

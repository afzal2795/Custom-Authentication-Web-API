using Food_Order_Custom_Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace Food_Order_Custom_Authentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationManager _customAuthenticationManager;
        private readonly FoodOrderAuthDbContext _dbContext;
        private readonly IDataProtector _protector;
        public AuthenticationController(
            ICustomAuthenticationManager customAuthenticationManager,
            IDataProtectionProvider protector,
            FoodOrderAuthDbContext dbContext)
        {
            _protector = protector.CreateProtector(GetType().FullName);
            _customAuthenticationManager = customAuthenticationManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return _dbContext.Foods;
        }

        [HttpPost]
        public IActionResult CreateFood(Food food)
        {
            _dbContext.Foods.Add(food);
            _dbContext.SaveChanges();
            return Ok("Successfully Created");
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register(User user)
        {
            user.Password = BC.HashPassword(user.Password);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);
            var token = _customAuthenticationManager.Authenticate(email, password, user);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost("Logout")]
        public IActionResult Unauthenticate()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            var status = _customAuthenticationManager.Unauthenticate(authorizationHeader);
            if (status)
                return Ok("Logged Out");
            return NotFound();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Food_Order_Custom_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FoodOrderAuthDbContext _dbContext;
        public UserController(FoodOrderAuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[HttpGet("RegisterUser")]
        //public User Register(User user)
        //{
        //    user.Password = BC.HashPassword(user.Password);
        //   _dbContext.Users.Add(user);
        //   _dbContext.SaveChanges();
        //    return user;
        //}

        [HttpGet("Login")]
        public StatusCodeResult test(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

            if (user == null || !BC.Verify(password, user.Password))
                return NotFound();
            else
                return NoContent();
        }

        //[HttpGet("ListUsers")]
        //[TokenAuthenticationFilter]
        //public List<User> userList()
        //{
        //    return _dbContext.Users.ToList();
        //}
    }
}

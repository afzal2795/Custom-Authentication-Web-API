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
    public class FoodController : ControllerBase
    {
        private readonly FoodOrderAuthDbContext _dbContext;
        public FoodController(FoodOrderAuthDbContext dbContext)
        {
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
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Successfully Created");
        }
    }
}

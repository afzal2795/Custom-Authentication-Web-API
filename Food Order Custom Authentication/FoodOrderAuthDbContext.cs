using Food_Order_Custom_Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Food_Order_Custom_Authentication
{
    public class FoodOrderAuthDbContext : DbContext
    {
        public FoodOrderAuthDbContext(DbContextOptions<FoodOrderAuthDbContext> contextOptions) : base(contextOptions)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}

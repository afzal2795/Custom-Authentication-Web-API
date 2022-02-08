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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.ClrType.Name);
            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                 .SelectMany(t => t.GetForeignKeys())
                 .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}

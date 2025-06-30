using CarShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Api.Context
{
    internal sealed class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; init; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

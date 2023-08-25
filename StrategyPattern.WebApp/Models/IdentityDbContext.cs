using Microsoft.EntityFrameworkCore;
using StrategyPattern.WebApp.Models;

namespace BaseProject.Models
{
    public class AppIdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}

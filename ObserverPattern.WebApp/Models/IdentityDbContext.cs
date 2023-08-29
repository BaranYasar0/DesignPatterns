using Microsoft.EntityFrameworkCore;
using ObserverPattern.WebApp.Models;

namespace BaseProject.Models
{
    public class AppIdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Discount> Discounts { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace BaseProject.Models
{
    public class AppIdentityDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext
    {
        public AppIdentityDbContext(DbContextOptions options) : base(options)
        {
        }


    }
}

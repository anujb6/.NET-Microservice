using Microservices.PlatformService.Models.EntityData;
using Microsoft.EntityFrameworkCore;

namespace Microservices.PlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
    }
}


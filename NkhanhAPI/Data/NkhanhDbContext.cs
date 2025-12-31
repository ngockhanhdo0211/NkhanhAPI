using Microsoft.EntityFrameworkCore;
using NkhanhAPI.Models;

namespace NkhanhAPI.Data
{
    public class NkhanhDbContext : DbContext
    {
        public NkhanhDbContext(DbContextOptions<NkhanhDbContext> options)
            : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}

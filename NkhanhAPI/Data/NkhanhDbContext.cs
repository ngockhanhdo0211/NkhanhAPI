using Microsoft.EntityFrameworkCore;
using NkhanhAPI.Models;
using NkhanhAPI.Models.Domain;

namespace NkhanhAPI.Data
{
    public class NkhanhDbContext : DbContext
    {
        public NkhanhDbContext(DbContextOptions<NkhanhDbContext> options)
            : base(options)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}

using NkhanhAPI.Models.Domain;
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
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== SEED DIFFICULTIES =====
            var difficulties = new List<Difficulty>
            {
                new Difficulty
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            // ===== SEED REGIONS =====
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Code = "HN",
                    Name = "Ha Noi"
                },
                new Region
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Code = "HCM",
                    Name = "Ho Chi Minh"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}

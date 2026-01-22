using Microsoft.EntityFrameworkCore;
using NkhanhAPI.Data;
using NkhanhAPI.Models.Domain;

namespace NkhanhAPI.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NkhanhDbContext dbContext;

        public WalkRepository(NkhanhDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // =========================
        // GET ALL + FILTER + SORT
        // =========================
        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true)
        {
            IQueryable<Walk> walks = dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region);

            // FILTER
            if (!string.IsNullOrWhiteSpace(filterOn) &&
                !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // SORT
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending
                        ? walks.OrderBy(x => x.Name)
                        : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending
                        ? walks.OrderBy(x => x.LengthInKm)
                        : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            return await walks.ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include(x => x.Difficulty)
                .Include(x => x.Region)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }

        // =========================
        // DELETE
        // =========================
        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FindAsync(id);

            if (walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }
    }
}

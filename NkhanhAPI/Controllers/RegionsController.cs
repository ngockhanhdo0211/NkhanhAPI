using Microsoft.AspNetCore.Mvc;
using NkhanhAPI.Data;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;

namespace NkhanhAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NkhanhDbContext dbContext;

        public RegionsController(NkhanhDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // GET ALL REGIONS
        // GET: /api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name
                });
            }

            return Ok(regionsDto);
        }
        // GET REGION BY ID
        // GET: /api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name
            };

            return Ok(regionDto);
        }
        // POST - CREATE REGION
        // POST: /api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto requestDto)
        {
            // Map DTO -> Domain
            var regionDomain = new Region
            {
                Code = requestDto.Code,
                Name = requestDto.Name
            };

            // Save to DB
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();

            // Map Domain -> DTO (response)
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        // PUT - UPDATE REGION
        // PUT: /api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateRegionRequestDto requestDto)
        {
            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Update domain
            regionDomain.Code = requestDto.Code;
            regionDomain.Name = requestDto.Name;

            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name
            };

            return Ok(regionDto);
        }
        // DELETE REGION
        // DELETE: /api/regions/{id} 
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            var regionDomain = dbContext.Regions.Find(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomain);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name
            };

            return Ok(regionDto);
        }
    }
}

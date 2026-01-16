using Microsoft.AspNetCore.Mvc;
using NkhanhAPI.Data;
using NkhanhAPI.Models.Domain;

namespace NkhanhAPI.Controllers
{
    // https: //localhost:1234/a[i/regions
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
        // GET: htpps://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            
            return Ok(regions);
        }
        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
    }
}

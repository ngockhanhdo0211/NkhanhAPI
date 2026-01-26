using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;
using NkhanhAPI.Repositories;

namespace NkhanhAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ⚠️ BẮT BUỘC LOGIN
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(
            IRegionRepository regionRepository,
            IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // =======================
        // GET: /api/regions
        // Reader + Writer
        // =======================
        [Authorize(Roles = "Reader,Writer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(regionsDto);
        }

        // =======================
        // GET: /api/regions/{id}
        // Reader + Writer
        // =======================
        [Authorize(Roles = "Reader,Writer")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // =======================
        // POST: /api/regions
        // Writer ONLY
        // =======================
        [Authorize(Roles = "Writer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regionDomain = mapper.Map<Region>(requestDto);

            regionDomain = await regionRepository.CreateAsync(regionDomain);

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById),
                new { id = regionDto.Id }, regionDto);
        }

        // =======================
        // PUT: /api/regions/{id}
        // Writer ONLY
        // =======================
        [Authorize(Roles = "Writer")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateRegionRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regionDomain = mapper.Map<Region>(requestDto);

            var updatedRegion = await regionRepository.UpdateAsync(id, regionDomain);

            if (updatedRegion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(updatedRegion));
        }

        // =======================
        // DELETE: /api/regions/{id}
        // Writer ONLY
        // =======================
        [Authorize(Roles = "Writer")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id);

            if (deletedRegion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(deletedRegion));
        }
    }
}

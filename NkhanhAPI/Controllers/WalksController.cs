using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;
using NkhanhAPI.Repositories;

namespace NkhanhAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ⚠️ BẮT BUỘC PHẢI LOGIN
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(
            IWalkRepository walkRepository,
            IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        // =======================
        // POST: /api/walks
        // Writer ONLY
        // =======================
        [Authorize(Roles = "Writer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walkDomain = mapper.Map<Walk>(requestDto);

            walkDomain = await walkRepository.CreateAsync(walkDomain);

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        // =======================
        // GET: /api/walks
        // Reader + Writer
        // =======================
        [Authorize(Roles = "Reader,Writer")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool isAscending = true)
        {
            var walksDomain = await walkRepository.GetAllAsync(
                filterOn,
                filterQuery,
                sortBy,
                isAscending);

            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);

            return Ok(walksDto);
        }

        // =======================
        // GET: /api/walks/{id}
        // Reader + Writer
        // =======================
        [Authorize(Roles = "Reader,Writer")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        // =======================
        // PUT: /api/walks/{id}
        // Writer ONLY
        // =======================
        [Authorize(Roles = "Writer")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateWalkRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var walkDomain = mapper.Map<Walk>(requestDto);

            var updatedWalk = await walkRepository.UpdateAsync(id, walkDomain);

            if (updatedWalk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(updatedWalk));
        }

        // =======================
        // DELETE: /api/walks/{id}
        // Writer ONLY
        // =======================
        [Authorize(Roles = "Writer")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);

            if (deletedWalk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(deletedWalk));
        }
    }
}

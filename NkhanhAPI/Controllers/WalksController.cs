using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;
using NkhanhAPI.Repositories;

namespace NkhanhAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // POST: /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto requestDto)
        {
            var walkDomain = mapper.Map<Walk>(requestDto);

            walkDomain = await walkRepository.CreateAsync(walkDomain);

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }
    }
}

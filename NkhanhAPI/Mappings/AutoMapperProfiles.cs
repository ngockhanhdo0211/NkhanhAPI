using AutoMapper;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;

namespace NkhanhAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // =====================
            // REGION
            // =====================
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();

            // =====================
            // WALK 🔥 BẮT BUỘC
            // =====================
            CreateMap<Walk, WalkDto>();
            CreateMap<AddWalkRequestDto, Walk>();
            CreateMap<UpdateWalkRequestDto, Walk>();
        }
    }
}

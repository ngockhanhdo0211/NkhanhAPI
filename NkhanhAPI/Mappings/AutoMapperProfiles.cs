using AutoMapper;
using NkhanhAPI.Models.Domain;
using NkhanhAPI.Models.DTO;
namespace NkhanhAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>();
            CreateMap<UpdateRegionRequestDto, Region>();
        }
    }
}

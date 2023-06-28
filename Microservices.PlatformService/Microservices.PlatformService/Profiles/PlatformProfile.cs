using AutoMapper;
using Microservices.PlatformService.Models.DTO;
using Microservices.PlatformService.Models.EntityData;
using PlatformService;

namespace Microservices.PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<PlatformCreateDTO, Platform>();
            CreateMap<PlatformReadDTO, PlatformPublishedDTO>();

            CreateMap<Platform, GrpcPlatformModel>()
             .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
        }
    }
}

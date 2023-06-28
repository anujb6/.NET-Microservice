using AutoMapper;
using Microservices.CommandService.Models.DTO_s;
using Grpc.Net.Client;
using Microservices.CommandService.Models;
using PlatformService;

namespace Microservices.CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile() 
        {
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandReadDTO>();
            
            CreateMap<PlatformPublishedDTO, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());

        }
    }
}

using AutoMapper;
using Grpc.Core;
using Microservices.PlatformService.Data.Abstracts;
using PlatformService;
using System.Threading.Tasks;

namespace Microservices.PlatformService.SyncDataServices.Grpc
{
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformservice _service;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformservice service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _service.GetAllPlatforms();

            foreach (var plat in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
            }

            return Task.FromResult(response);
        }
    }
}

using Microservices.PlatformService.Models.DTO;
using System.Threading.Tasks;

namespace Microservices.PlatformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDTO plat);
    }
}
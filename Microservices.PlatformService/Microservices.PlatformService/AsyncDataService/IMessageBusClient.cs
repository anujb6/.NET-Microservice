using Microservices.PlatformService.Models.DTO;
using RabbitMQ.Client;

namespace Microservices.PlatformService.AsyncDataService
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDTO platformPublishedDto);
    }
}
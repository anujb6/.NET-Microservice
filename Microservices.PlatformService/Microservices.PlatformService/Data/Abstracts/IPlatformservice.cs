using Microservices.PlatformService.Models.EntityData;
using System.Collections;
using System.Collections.Generic;

namespace Microservices.PlatformService.Data.Abstracts
{
    public interface IPlatformservice
    {
        bool SaveChanges();

        List<Platform> GetAllPlatforms();

        Platform GetPlatformById(int id);

        Platform CreatePlatform(Platform platform);
    }
}

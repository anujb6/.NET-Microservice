using Microservices.CommandService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using Microservices.CommandService.Data.Abstracts;
using Microservices.CommandService.SyncDataServices.Grpc;

namespace Microservices.CommandService.Data.DBprep
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpClient.ReturnAllPlatforms();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);

            }
        }

        private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
        {
            Console.WriteLine("Seeding new platforms...");

            if(platforms != null)
                foreach (var plat in platforms)
                {
                    if (!repo.ExternalPlatformExists(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                    }
                    repo.SaveChanges();
                }
        }

    }
}

using System.Collections.Generic;
using System;
using Microservices.PlatformService.Data.Abstracts;
using System.Linq;
using Microservices.PlatformService.Models.EntityData;

namespace Microservices.PlatformService.Data.Dataservice
{
    public class Platformservice : IPlatformservice
    {
        private readonly AppDbContext _context;
        public Platformservice(AppDbContext context)
        {
            _context = context;
        }

        public Platform CreatePlatform(Platform platform)
        {
            if (platform == null)
                throw new ArgumentNullException(nameof(platform));

            var entity = _context.Add(platform);
            return entity.Entity;
        }

        public List<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

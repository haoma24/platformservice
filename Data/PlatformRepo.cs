using Microsoft.Extensions.Caching.StackExchangeRedis;
using PlatformService.Data.Caching;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        private readonly IRedisCachingService _redisCache;
        public PlatformRepo(AppDbContext context, IRedisCachingService redisCache)
        {
            _context = context;
            _redisCache = redisCache;
        }

        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentNullException(nameof(plat));
            }
            _context.Platforms.Add(plat);
        }

        public void DeletePlatform(Platform plat)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void UpdatePlatform(Platform plat)
        {
            throw new NotImplementedException();
        }
    }
}
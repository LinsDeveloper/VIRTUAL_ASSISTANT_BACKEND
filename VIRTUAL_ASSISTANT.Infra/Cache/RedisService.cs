using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Interfaces.Cache;

namespace VIRTUAL_ASSISTANT.Infra.Cache
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisService(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public async Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(key, value, expiry);
        }

        public async Task<string> GetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}

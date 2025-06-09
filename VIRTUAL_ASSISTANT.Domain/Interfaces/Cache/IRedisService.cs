using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.Interfaces.Cache
{
    public interface IRedisService : IDisposable
    {
        Task<bool> SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<string> GetAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task<bool> RemoveAsync(string key);
    }
}

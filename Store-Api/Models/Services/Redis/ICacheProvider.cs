using Microsoft.OpenApi.Any;

namespace StoreApi.Models.Services.Redis
{
    public interface ICacheProvider
    {
        Task SetCacheAsync(string key, dynamic value);
        Task<dynamic> GetCacheAsync(string key);
        void Subscribe(string message);
    }
}

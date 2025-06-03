using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Excel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json;
using StackExchange.Redis;
using StoreApi.BLL.Features.ProductFeature.Query.GetByIdProduct;
using StoreApi.Entity._User;
using System.Web.Razor.Editor;

namespace StoreApi.Models.Services.Redis
{
    public class CacheProvider : ICacheProvider
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly ISubscriber _subscriber;
        private readonly IDatabase _redis;

        public CacheProvider(IConnectionMultiplexer connection)
        {
            _connection = connection;
            _subscriber = connection.GetSubscriber();
            _redis = connection.GetDatabase();
        }

        public async Task<dynamic> GetCacheAsync(string key)
        {
            var val = await _redis.StringGetAsync(key);
            if (val.HasValue)
            {
                var res = JsonConvert.DeserializeObject<GetByIdProductViewModel>(val);
                return res;
            }
            return null;
        }

        public async Task SetCacheAsync(string key, dynamic value)
        {
            string strValue= JsonConvert.SerializeObject(value);
            await _redis.StringSetAsync(key , strValue);
        }

        public void Subscribe(string message)
        {
            _subscriber.Publish("My-Redis" , message);
        }
    }
}

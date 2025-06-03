
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace StoreApi.Models.Services.Redis
{
    public class RedisSyncService : BackgroundService
    {
        //private readonly IDistributedCache _redis;
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _redis;

        public RedisSyncService( IConnectionMultiplexer connection)
        {
            _redis = connection.GetDatabase();
            _connection = connection;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sub = _connection.GetSubscriber();
            sub.Subscribe("My-Redis", (Channel,key) =>
            {
                _redis.KeyDelete(key.ToString());
            });
            return Task.CompletedTask;
        }
    }
}

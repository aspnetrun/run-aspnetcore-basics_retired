using Basket.API.Entities;
using Basket.API.Repositories.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    // https://dotnetcorecentral.com/blog/redis-cache-in-net-core-docker-container/
    // https://stackexchange.github.io/StackExchange.Redis/Configuration.html
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ILogger<RedisBasketRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<RedisBasketRepository>();
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();

            return data?.Select(k => k.ToString());
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {            
            var data = await _database.StringGetAsync(customerId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));

            if (!created)
            {
                _logger.LogInformation("Problem occur persisting the item.");
                return null;
            }

            _logger.LogInformation("Basket item persisted succesfully.");

            return await GetBasketAsync(basket.BuyerId);
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());


            //var customerBasket = new CustomerBasket();
            //customerBasket.BuyerId = "swn";
            //customerBasket.Items.Add(
            //    new BasketItem
            //    {
            //        Id = "1",
            //        ProductId = 2,
            //        OldUnitPrice = 3,
            //        PictureUrl = "asd",
            //        ProductName = "bas",
            //        Quantity = 3,
            //        UnitPrice = 4
            //    }
            //    );

            //var ser = JsonConvert.SerializeObject(customerBasket);
            //var created = _database.StringSet(customerBasket.BuyerId, ser);
            //var getResult = _database.StringGet(customerBasket.BuyerId);
            //var newObject = JsonConvert.DeserializeObject<CustomerBasket>(getResult);
        }


    }
}

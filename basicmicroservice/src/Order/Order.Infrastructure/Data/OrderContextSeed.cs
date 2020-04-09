using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext aspnetrunContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                // aspnetrunContext.Database.Migrate();
                // aspnetrunContext.Database.EnsureCreated();

                if (!aspnetrunContext.Orders.Any())
                {
                    aspnetrunContext.Orders.AddRange(GetPreconfiguredOrders());
                    await aspnetrunContext.SaveChangesAsync();
                }
              
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(aspnetrunContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Order.Core.Entities.Order> GetPreconfiguredOrders()
        {
            return new List<Order.Core.Entities.Order>()
            {
                new Order.Core.Entities.Order() { FirstName = "swn", LastName = "asd", AddressLine = "bahcelievler" },
                new Order.Core.Entities.Order() { FirstName = "swn2", LastName = "asd2", AddressLine = "bahcelievler2" }                
            };
        }        
    }
}

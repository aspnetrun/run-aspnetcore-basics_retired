using AspNetRunBasic.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetRunBasic.Data
{
    public class AspnetRunContextSeed
    {
        public static async Task SeedAsync(AspnetRunContext aspnetrunContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                // TODO: Only run this if using a real database
                // aspnetrunContext.Database.Migrate();
                // aspnetrunContext.Database.EnsureCreated();

                if (!aspnetrunContext.Products.Any())
                {
                    aspnetrunContext.Products.AddRange(GetPreconfiguredProducts());
                    await aspnetrunContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<AspnetRunContextSeed>();
                    log.LogError(exception.Message);
                    await SeedAsync(aspnetrunContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product() { Name = "IPhone X", Description = "IPhone X Well Done", Status = ProductStatus.InStock },                
                new Product() { Name = "Samsung 10", Description = "Samsung X Well Done", Status = ProductStatus.InStock },                
                new Product() { Name = "Huawei Plus", Description = "Huawei X Well Done", Status = ProductStatus.InStock }
            };
        }
    }
}

using Order.Core.Repositories;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repository
{
    public class OrderRepository : Repository<Order.Core.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order.Core.Entities.Order>> GetOrderListAsync()
        {            
            // second way
            return await GetAllAsync();
        }

    }
}

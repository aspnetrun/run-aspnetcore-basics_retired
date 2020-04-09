using Order.Core.Repositories.Base;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Repositories
{
    public interface IOrderRepository : IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrderListAsync();
    }
}

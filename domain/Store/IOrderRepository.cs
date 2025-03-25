using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync();

        Task<Order> GetByIdAsync(int id);

        Task UpdateAsync(Order order);

        Task<List<Order>> GetOrdersAsync(List<int> ids);
    }
}

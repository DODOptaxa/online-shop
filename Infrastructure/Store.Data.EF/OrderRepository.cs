using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    
    public class OrderRepository : IOrderRepository
    {
        StoreDbContextFactory contextFactory;

        public OrderRepository(StoreDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public Order Create()
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

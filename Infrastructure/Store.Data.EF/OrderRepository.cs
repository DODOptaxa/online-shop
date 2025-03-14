using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    
    public class OrderRepository : IOrderRepository
    {
        StoreDbContextFactory dbContextFactory;

        public OrderRepository(StoreDbContextFactory contextFactory)
        {
            this.dbContextFactory = contextFactory;
        }
        public Order Create()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = Order.DtoFactory.Create();
            dbContext.Orders.Add(dto);
            dbContext.SaveChanges();

            return Order.Mapper.Map(dto);
        }

        public Order GetById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = dbContext.Orders
                               .Include(order => order.Items)
                               .SingleOrDefault(order => order.Id == id);

            if (dto == null) throw new InvalidDataException() ;

            return Order.Mapper.Map(dto);
        }

        public void Update(Order order)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            dbContext.SaveChanges();
        }
    }
}

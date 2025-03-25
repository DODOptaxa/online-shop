using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF.Store
{
    
    public class OrderRepository : IOrderRepository
    {
        StoreDbContextFactory dbContextFactory;

        public OrderRepository(StoreDbContextFactory contextFactory)
        {
            this.dbContextFactory = contextFactory;
        }
        public async Task<Order> CreateAsync()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = Order.DtoFactory.Create();
            await dbContext.Orders.AddAsync(dto);
            await dbContext.SaveChangesAsync();

            return Order.Mapper.Map(dto);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = await dbContext.Orders
                                     .Include(order => order.Items)
                                     .SingleOrDefaultAsync(order => order.Id == id);

            if (dto == null) throw new InvalidDataException();

            return Order.Mapper.Map(dto);
        }

        public async Task<List<Order>> GetOrdersAsync(List<int> ids)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));
            var dtos = await dbContext.Orders
                                      .Where(a => ids.Contains(a.Id))
                                      .Include(order => order.Items)
                                      .ToListAsync();

            if (dtos.Count == 0) return null;

            return dtos.Select(Order.Mapper.Map).ToList();
        }

        public async Task UpdateAsync(Order order)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            // Припускаю, що об'єкт order уже відстежується контекстом або оновлюється через DTO.
            // Якщо потрібне явне оновлення, додайте логіку перед SaveChangesAsync.
            await dbContext.SaveChangesAsync();
        }
    }
}

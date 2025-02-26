using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderItem
    {
        private readonly OrderItemDto dto;
        public int BookId => dto.BookId;

        public uint Count 
        {
            get { return dto.Count; }
            set
            {
                dto.Count = value;
            } 
        }

        public decimal Price 
        {
            get { return dto.Price; }
            set
            {
                dto.Price = value;
            }
        }

        internal OrderItem(OrderItemDto dto)
        {
            this.dto = dto;
        }

        public static class DtoFactory
        {
            public static OrderItemDto Create(OrderDto order, int bookId, decimal price, uint count)
            {
                if (order == null)
                    throw new ArgumentNullException(nameof(order));

                return new OrderItemDto
                {
                    BookId = bookId,
                    Price = price,
                    Count = count,
                    Order = order,
                };
            }
        }

        public static class Mapper
        {
            public static OrderItem Map(OrderItemDto dto) => new OrderItem(dto);

            public static OrderItemDto Map(OrderItem domain) => domain.dto;
        }
    }
}

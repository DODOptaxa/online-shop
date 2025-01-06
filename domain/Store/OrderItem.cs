using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderItem
    {
        public int BookId { get; }

        public uint Count { get; set; }

        public decimal Price { get; }

        public OrderItem(int bookId, uint count, decimal price)
        {

            BookId = bookId;
            Count = count;
            Price = price;

        }
    }
}

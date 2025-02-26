using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data
{
    public class OrderItemDto
    {
        public int Id { get; set; } 
        public int BookId { get; set; }

        public uint Count { get; set; }

        public decimal Price { get; set; }

        public OrderDto Order { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data
{
    public class OrderDto
    {
        public int Id { get; set; }

        public IList<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

        public string? CellPhone { get; set; }

        //------------------------------
        public string? DeliveryCode { get; set; }

        public string? DeliveryDescription { get; set; }

        public decimal DeliveryAmount { get; set; }

        public Dictionary<string, string>? DeliveryParameters { get; set; }

        public string? PaymentCode { get; set; }

        public string? PaymentDescription { get; set; }


        public Dictionary<string, string>? PaymentParameters { get; set; }

        public int? UserId { get; set; }

    }

}

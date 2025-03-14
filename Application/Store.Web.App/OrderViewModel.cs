using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public OrderItemViewModel[] Items { get; set; } = new OrderItemViewModel[0];

        public uint TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public string CellPhone { get; set; }

        public string DeliveryDescription { get; set; }

        public string PaymentDescription { get; set; }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

    }
}

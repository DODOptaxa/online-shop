namespace Store.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public OrderItemViewModel[] Items { get; set; } = new OrderItemViewModel[0];

        public uint TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}

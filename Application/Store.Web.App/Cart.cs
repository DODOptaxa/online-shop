namespace Store.Web.App
{
    public class Cart
    {
        public int OrderId { get; }

        public uint TotalCount { get; }

        public decimal TotalPrice { get; }

        public Cart(int orderId, uint totalCount, decimal totalPrice)
        {
            OrderId = orderId;
            TotalCount = totalCount;
            TotalPrice = totalPrice;
        }
    }
}
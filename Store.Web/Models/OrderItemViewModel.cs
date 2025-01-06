namespace Store.Web.Models
{
    public class OrderItemViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public uint Count { get; set; }

        public decimal Price { get; set; }



    }
}

using Store.Contractors;

namespace Store.Web.Models
{
    public class DeliveryDetailsViewModel
    {
        public Form Form { get; set; }

        public int OrderId { get { return Form.OrderId; } }

        public IReadOnlyDictionary<string, string> DeliveryContractors { get; set; }
    }
}

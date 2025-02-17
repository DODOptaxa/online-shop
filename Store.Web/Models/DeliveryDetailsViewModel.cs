using Store.Contractors;

namespace Store.Web.Models
{
    public class DeliveryDetailsViewModel
    {
        public Form DeliveryForm { get; set; }

        public Form PaymentForm { get; set; }

        public int OrderId { get { return DeliveryForm.OrderId; } }

        public IReadOnlyDictionary<string, string> DeliveryContractors { get; set; }

        public IReadOnlyDictionary<string, string> PaymentContractors { get; set; }
    }
}

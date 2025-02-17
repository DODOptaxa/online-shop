using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public class CashPaymentService : IPaymentService
    {
        public string Code => "Cash";

        public string Title => "Оплата при получении";

        public Form CreateForm(int orderId)
        {
            return new Form(Code, orderId, new Field[0]);
        }

        public OrderPayment CreatePayment(Form form)
        {
            if (form.Code != Code)
                throw new InvalidOperationException("Invalid payment form.");
            return new OrderPayment(Code, "Оплата (а) налом", new Dictionary<string, string>());
        }

        public Form CreateUpdatedForm(int orderId, IReadOnlyDictionary<string, string> values)
        {
            return new Form(Code, orderId, new Field[0]);
        }
    }
}

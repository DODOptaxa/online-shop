using Store.Web.Contractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors.RoboKassa
{
    public class RoboKassaPaymentService : IPaymentService, IWebContractorService
    {
        public string Code => "RoboKassa";

        public string Title => "Оплата картою";

        public string GetUri => "RoboKassa";

        public Form CreateForm(int orderId)
        {
            return new Form(Code, orderId, new Field[0]);
        }

        public OrderPayment CreatePayment(Form form)
        {
            return new OrderPayment(Code, "Оплата картою", new Dictionary<string, string>());
        }

        public Form CreateUpdatedForm(int orderId, IReadOnlyDictionary<string, string> values)
        {
            return new Form(Code, orderId, new Field[0]);
        }
    }
}

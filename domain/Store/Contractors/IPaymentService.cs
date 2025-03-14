using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public interface IPaymentService
    {
        string Code { get; }

        string Title { get; }

        Form CreateForm(int orderId);

        Form CreateUpdatedForm(int orderId, IReadOnlyDictionary<string, string> values);

        OrderPayment CreatePayment(Form form);

    }
}

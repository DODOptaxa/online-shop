using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Contractors
{
    public interface IDeliveryService
    {
        string Code { get; }

        string Title { get; }

        Form CreateForm(int orderId);

        Form CreateUpdatedForm(int orderId, IReadOnlyDictionary<string, string> values);

        OrderDelivery CreateDelivery(Form form);

    }
}

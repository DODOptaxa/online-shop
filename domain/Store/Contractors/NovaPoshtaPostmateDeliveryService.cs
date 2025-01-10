namespace Store.Contractors
{
    public class NovaPoshtaPostmateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
        {
            { "1", "Київ" },
            { "2", "Харків" }
        };

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates =
            new Dictionary<string, IReadOnlyDictionary<string, string>>
            {
                { "1", new Dictionary<string, string>
                    {
                        { "1", "Київ, вул. Пирогівський шлях, 135" },
                        { "2", "Київ, вул. Слобожанська, 13" }
                    }
                },
                { "2", new Dictionary<string, string>
                    {
                        { "1", "Харків, вул. Польова, 67" },
                        { "2", "Харків, вул. Достоєвського, 5" }
                    }
                }
            };

        public string Code => "Postmate";

        public string Title => "Поштомат Нова Пошта";

        public Form CreateForm(int orderId)
        {
            return new Form(Code, orderId, new[]
            {
                new Field("Город", "city", "1", false ,cities),
                new Field("Поштомат", "postamate", "1", false , postamates["1"]),
            });
        }
        public Form CreateUpdatedForm(int orderId, IReadOnlyDictionary<string, string> values)
        {
                if (values["city"] != null && values["postamate"] != null)
                {
                    return new Form(Code, orderId, new Field[]
                    {
                        new Field("Город", "city", values["city"],true, cities),
                        new Field("Постамат", "postamate", values["postamate"],true, postamates[values["city"]]),
                    });
                }
                else
                    throw new InvalidOperationException("Invalid postamate or city.");
            }
    }
}

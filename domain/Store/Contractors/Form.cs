
namespace Store.Contractors
{
    public class Form
    {
        public string Code { get; }
        public int OrderId { get; }
        public IReadOnlyList<Field> Fields { get; }
        public Form(string uniqueCode, int orderId, IEnumerable<Field> fields)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
                throw new ArgumentException(nameof(uniqueCode));
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));
            Code = uniqueCode;
            OrderId = orderId;
            Fields = fields.ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> _items;

        //------------------------------------

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            Id = id;
            _items = new List<OrderItem>(items);
        }

        //------------------------------------
 
        public IReadOnlyCollection<OrderItem> Items
        {
            get { return _items; }
        }

        public uint TotalCount
        {
            get { return (uint)Items.Sum(item => item.Count); }
        }

        public decimal TotalPrice
        {
            get { return _items.Sum(item => item.Price * item.Count); }
        }

        public void AddItem(Book book, uint count)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof (book));
            }

            var item = _items.SingleOrDefault(x => x.BookId == book.Id);

            if (item == null)
            {
                _items.Add(new OrderItem(book.Id, count, book.Price));
            }
            else
            {
                _items.Remove(item);
                _items.Add(new OrderItem(book.Id, item.Count + count, book.Price));
            }
        }
    }
}

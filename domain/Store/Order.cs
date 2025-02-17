using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> _items;

        public string CellPhone { get; set; }

        public OrderDelivery Delivery { get; set; }

        public OrderPayment Payment { get; set; }
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
            get { return _items.Sum(item => item.Price * item.Count) + (Delivery?.Amount ?? 0); }
        }

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


        public void RemoveItem(int bookId)
        {
            int index = _items.FindIndex(item => item.BookId == bookId);
            if (index == -1)
                throw new InvalidOperationException("Order does not contain specified item." + bookId);
            if (_items[index].Count - 1 <= 0)
                _items.RemoveAt(index);

            else _items[index].Count -= 1;
        }

        public void RemoveItems(int bookId)
        {
            int index = _items.FindIndex(item => item.BookId == bookId);
            if (index == -1)
                throw new InvalidOperationException("Order does not contain specified item." + bookId);
            _items.RemoveAll(x => x.BookId == bookId);
        }


        public void AddItem(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof (book));
            }

            int index = _items.FindIndex(item => item.BookId == book.Id);
            if (index == -1)
                _items.Add(new OrderItem(book.Id, 1, book.Price));
            else
                _items[index].Count += 1;
        }

        public OrderItem GetItem(int bookId)
        {
            int index = _items.FindIndex(item => item.BookId == bookId);
            if (index == -1)
                throw new InvalidOperationException("Book not found: " + bookId);

            return _items[index];
        }
    }
}

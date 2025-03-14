using Store.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class OrderItemCollection : IReadOnlyCollection<OrderItem>
    {
        private readonly OrderDto orderDto;
        private readonly List<OrderItem> items;

        public OrderItemCollection(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ArgumentNullException(nameof(orderDto));

            this.orderDto = orderDto;

            items = orderDto.Items
                            .Select(OrderItem.Mapper.Map)
                            .ToList();
        }

        public int Count => items.Count;

        public IEnumerator<OrderItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (items as IEnumerable).GetEnumerator();
        }

        public OrderItem Get(int bookId)
        {
            if (TryGet(bookId, out OrderItem orderItem))
                return orderItem;

            throw new InvalidOperationException("Book not found.");
        }

        public bool TryGet(int bookId, out OrderItem orderItem)
        {
            var index = items.FindIndex(item => item.BookId == bookId);
            if (index == -1)
            {
                orderItem = null;
                return false;
            }

            orderItem = items[index];
            return true;
        }

        public bool TryFind(int bookId, out int index)
        {
            index = items.FindIndex(item => item.BookId == bookId);

            return index != -1 ? true : false;
        }

        public void Add(int bookId, decimal price)
        {
            if (TryFind(bookId, out int index)) 
            {
                items[index].Count += 1;
            }
            else { 
                var orderItemDto = OrderItem.DtoFactory.Create(orderDto, bookId, price, 1);
                orderDto.Items.Add(orderItemDto);

                var orderItem = OrderItem.Mapper.Map(orderItemDto);
                items.Add(orderItem);
            }

        }

        public void RemoveItem(int bookId)
        {
            if (!TryFind(bookId, out int index))
                throw new InvalidOperationException("Order does not contain specified item." + bookId);
            if (items[index].Count - 1 <= 0)
            {
                items.RemoveAt(index);
                orderDto.Items.RemoveAt(index);
            }

            else
            {
                items[index].Count -= 1;
            }
        }

        public void RemoveItems(int bookId)
        {
            if (!TryFind(bookId, out int index))
                throw new InvalidOperationException("Order does not contain specified item." + bookId);
            items.RemoveAt(index);
            orderDto.Items.RemoveAt(index);
        }

    }
}
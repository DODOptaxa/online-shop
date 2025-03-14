using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using Store.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Web.App
{
    public class OrderService
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        protected ISession Session => httpContextAccessor.HttpContext.Session;

        public OrderService(IBookRepository bookRepository,
                            IOrderRepository orderRepository,
                            INotificationService notificationService,
                            IHttpContextAccessor httpContextAccessor)
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        private const string UAcode = "+38";

        public bool TryGetModel(out OrderViewModel model)
        {
            if (TryGetOrder(out Order order))
            {
                model = Map(order);
                return true;
            }

            model = null;
            return false;
        }

        internal bool TryGetOrder(out Order order)
        {
            if (Session.TryGetCart(out Cart cart))
            {
                order = orderRepository.GetById(cart.OrderId);
                return true;
            }

            order = null;
            return false;
        }

        internal OrderViewModel Map(Order order)
        {
            var books = GetBooks(order);
            var items = from item in order.Items
                        join book in books on item.BookId equals book.Id
                        select new OrderItemViewModel
                        {
                            BookId = book.Id,
                            Title = book.Title,
                            Author = book.Author,
                            Price = item.Price,
                            Count = item.Count,
                        };

            return new OrderViewModel
            {
                Id = order.Id,
                Items = items.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
                CellPhone = RemovePlus38Prefix(order.CellPhone),
                DeliveryDescription = order.Delivery?.Description,
                PaymentDescription = order.Payment?.Description
            };
        }

        public bool TryGetOrderItem(int id, out OrderItem item)
        {
            if (TryGetOrder(out Order order))
            {
                item = order.Items.Get(id);
                return true;
            }
            else{
                item = null;
                return false;
            }
        }

        public string RemovePlus38Prefix(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.StartsWith(UAcode))
            {
                return phoneNumber.Substring(3);
            }
            return phoneNumber;
        }

        public string AddPlus38Prefix(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber) && !phoneNumber.StartsWith(UAcode))
            {
                return UAcode + phoneNumber;
            }
            return phoneNumber;
        }

        internal IEnumerable<Book> GetBooks(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);

            return bookRepository.GetAllByIds(bookIds);
        }

        public OrderViewModel AddBook(int bookId)
        {
            Console.WriteLine("Stage1");

            if (!TryGetOrder(out Order order))
                order = orderRepository.Create();

            AddOrUpdateBook(order, bookId);
            UpdateSession(order);

            return Map(order);
        }

        internal void AddOrUpdateBook(Order order, int bookId)
        {
            var book = bookRepository.GetById(bookId);
            order.Items.Add(book.Id, book.Price);

            orderRepository.Update(order);
        }

        internal void UpdateSession(Order order)
        {
            var cart = new Cart(order.Id, order.TotalCount, order.TotalPrice);
            Session.Set(cart);
        }

        public OrderViewModel UpdateBook(int bookId, uint count)
        {
            var order = GetOrder();
            order.Items.Get(bookId).Count = count;

            orderRepository.Update(order);
            UpdateSession(order);

            return Map(order);
        }

        public OrderViewModel RemoveBook(int bookId)
        {
            Console.WriteLine("REMOVE BOOK");
            var order = GetOrder();
            order.Items.RemoveItem(bookId);

            orderRepository.Update(order);
            UpdateSession(order);

            return Map(order);
        }

        public OrderViewModel RemoveBooks(int bookId)
        {
            var order = GetOrder();
            order.Items.RemoveItems(bookId);

            orderRepository.Update(order);
            UpdateSession(order);

            return Map(order);
        }

        public Order GetOrder()
        {
            if (TryGetOrder(out Order order))
                return order;

            throw new InvalidOperationException("Empty session.");
        }

        public OrderViewModel SendConfirmation(string cellPhone)
        {
            var order = GetOrder();
            var model = Map(order);
            cellPhone = AddPlus38Prefix(cellPhone);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 1111; 
                Session.SetInt32(formattedPhone, confirmationCode);
                Console.WriteLine(formattedPhone +"<- CURRENT PHONE IN CACHE");
                notificationService.SendConfirmationCode(formattedPhone, confirmationCode);

                //--------------------------------------------------
                model.CellPhone = RemovePlus38Prefix(formattedPhone);
                //--------------------------------------------------
            }
            else
                model.Errors["cellPhone"] = "ваш номер телефону не відповідає формату +38-000-000-0000";

            return model;
        }

        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        internal bool TryFormatPhone(string cellPhone, out string formattedPhone)
        {
            try
            {
                Console.WriteLine(cellPhone);
                var phoneNumber = phoneNumberUtil.Parse(cellPhone, "UA");

                if (!phoneNumberUtil.IsValidNumberForRegion(phoneNumber, "UA"))
                {
                    formattedPhone = null;
                    return false;
                }

                string nationalNumber = phoneNumber.NationalNumber.ToString();
                string countryCode = "+" + phoneNumber.CountryCode;


                if (nationalNumber.Length == 9) 
                {
                    formattedPhone = countryCode + nationalNumber;
                }
                else 
                {
                    formattedPhone = null;
                    return false;
                }

                return true;
            }
            catch (NumberParseException)
            {
                formattedPhone = null;
                return false;
            }
        }

        public OrderViewModel ConfirmCellPhone(string cellPhone, int confirmationCode)
        {
            cellPhone = AddPlus38Prefix(cellPhone);
            Console.WriteLine(cellPhone + "<- CURRENT PHONE IN CONFIRMATION");
            int? storedCode = Session.GetInt32(cellPhone);
            var model = new OrderViewModel();

            if (storedCode == null)
            {
                model.Errors["cellPhone"] = "Щось трапилось, спробуйте знову.";
                return model;
            }

            if (storedCode != confirmationCode)
            {
                model.Errors["confirmationCode"] = "Невірний код, спробуйте ще раз.";
                return model;
            }

            var order = GetOrder();
            order.CellPhone = cellPhone;
            orderRepository.Update(order);

            Session.Remove(cellPhone);

            return Map(order);
        }

        public OrderViewModel SetDelivery(OrderDelivery delivery)
        {
            var order = GetOrder();
            order.Delivery = delivery;
            orderRepository.Update(order);

            return Map(order);
        }

        public OrderViewModel SetPayment(OrderPayment payment)
        {
            var order = GetOrder();
            order.Payment = payment;
            orderRepository.Update(order);
            Session.RemoveCart();

            return Map(order);
        }
    }
}

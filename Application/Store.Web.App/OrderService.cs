using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using Store.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Store.Data.EF.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;



namespace Store.Web.App
{
    public class OrderService
    {
        private readonly IBookRepository bookRepository;
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;


        protected ISession Session => httpContextAccessor.HttpContext.Session;

        public OrderService(IBookRepository bookRepository,
                            IOrderRepository orderRepository,
                            INotificationService notificationService,
                            IHttpContextAccessor httpContextAccessor,
                            IServiceProvider _serviceProvider
                            )
        {
            this.bookRepository = bookRepository;
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
            this.httpContextAccessor = httpContextAccessor;
            this._serviceProvider = _serviceProvider;
        }

        private const string UAcode = "+38";

        public async Task<(bool hasValue, OrderViewModel model)> TryGetModelAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();
            if (hasValue)
                return (true, await MapAsync(order));

            return (false, null);
        }

        internal async Task<(bool hasValue, Order order)> TryGetOrderAsync()
        {
            if (Session.TryGetCart(out Cart cart))
            {
                var order = await orderRepository.GetByIdAsync(cart.OrderId);
                return (true, order);
            }

            return (false, null);
        }

        public async Task<OrderViewModel> MapAsync(Order order)
        {
            var books = await GetBooksAsync(order);
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

        public async Task<(bool hasValue, OrderItem item)> TryGetOrderItemAsync(int id)
        {
            var (hasValue, order) = await TryGetOrderAsync();
            if (hasValue)
            {
                var item = order.Items.Get(id);
                return (true, item);
            }
            else
            {
                return (false, null);
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

        internal async Task<IEnumerable<Book>> GetBooksAsync(Order order)
        {
            var bookIds = order.Items.Select(item => item.BookId);
            return await bookRepository.GetAllByIdsAsync(bookIds);
        }

        public async Task<OrderViewModel> AddBookAsync(int bookId)
        {
            Console.WriteLine("Stage1");

            var (hasValue, order) = await TryGetOrderAsync();
            if (!hasValue)
            {
                order = await orderRepository.CreateAsync();

                using (var scope = _serviceProvider.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    User user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
                    if (user == null) throw new InvalidOperationException("User not found.");
                    user.OrderIds ??= new List<int>();
                    user.OrderIds.Add(order.Id);
                    await userManager.UpdateAsync(user);
                }

            }

            await AddOrUpdateBookAsync(order, bookId);
            await UpdateSessionAsync(order);

            return await MapAsync(order);
        }

        internal async Task AddOrUpdateBookAsync(Order order, int bookId)
        {
            var book = await bookRepository.GetByIdAsync(bookId);
            order.Items.Add(book.Id, book.Price);
            await orderRepository.UpdateAsync(order);
        }

        internal async Task UpdateSessionAsync(Order order)
        {
            var cart = new Cart(order.Id, order.TotalCount, order.TotalPrice);
            Session.Set(cart);
        }

        public async Task<OrderViewModel> UpdateBookAsync(int bookId, uint count)
        {
            var order = await GetOrderAsync();
            order.Items.Get(bookId).Count = count;

            await orderRepository.UpdateAsync(order);
            await UpdateSessionAsync(order);

            return await MapAsync(order);
        }

        public async Task<OrderViewModel> RemoveBookAsync(int bookId)
        {
            Console.WriteLine("REMOVE BOOK");
            var order = await GetOrderAsync();
            order.Items.RemoveItem(bookId);

            await orderRepository.UpdateAsync(order);
            await UpdateSessionAsync(order);

            return await MapAsync(order);
        }

        public async Task<OrderViewModel> RemoveBooksAsync(int bookId)
        {
            var order = await GetOrderAsync();
            order.Items.RemoveItems(bookId);

            await orderRepository.UpdateAsync(order);
            await UpdateSessionAsync(order);

            return await MapAsync(order);
        }

        public async Task<Order> GetOrderAsync()
        {
            var(hasValue, order) = await TryGetOrderAsync();
            if (hasValue)
                return order;

            throw new InvalidOperationException("Empty session.");
        }

        public async Task<OrderViewModel> SendConfirmationAsync(string cellPhone)
        {
            var order = await GetOrderAsync();
            var model = await MapAsync(order);
            cellPhone = AddPlus38Prefix(cellPhone);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 1111;
                Session.SetInt32(formattedPhone, confirmationCode);
                Console.WriteLine(formattedPhone + "<- CURRENT PHONE IN CACHE");
                notificationService.SendConfirmationCode(formattedPhone, confirmationCode);

                model.CellPhone = RemovePlus38Prefix(formattedPhone);
            }
            else
            {
                model.Errors["cellPhone"] = "ваш номер телефону не відповідає формату +38-000-000-0000";
            }

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

        public async Task<OrderViewModel> ConfirmCellPhoneAsync(string cellPhone, int confirmationCode)
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

            var order = await GetOrderAsync();
            order.CellPhone = cellPhone;
            await orderRepository.UpdateAsync(order);

            Session.Remove(cellPhone);

            return await MapAsync(order);
        }

        public async Task<OrderViewModel> SetDeliveryAsync(OrderDelivery delivery)
        {
            var order = await GetOrderAsync();
            order.Delivery = delivery;
            await orderRepository.UpdateAsync(order);

            return await MapAsync(order);
        }

        public async Task<OrderViewModel> SetPaymentAsync(OrderPayment payment)
        {
            var order = await GetOrderAsync();
            order.Payment = payment;
            await orderRepository.UpdateAsync(order);
            Session.RemoveCart();

            return await MapAsync(order);
        }

        
    }
}

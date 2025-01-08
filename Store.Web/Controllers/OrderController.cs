using Microsoft.AspNetCore.Mvc;
using Store.Memory;
using Store.Messages;
using Store.Web.Models;
using System.Text.RegularExpressions;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository,
                                INotificationService notificationService)
                                
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _notificationService = notificationService;
        }

        private OrderViewModel Map(Order order)
        {
            var booksIds = order.Items.Select(item => item.BookId);
            var books = _bookRepository.GetAllByIds(booksIds);
            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderItemViewModel
                             { 
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Count = item.Count,
                                 Price = item.Price,
                             };
            return new OrderViewModel
            {
                Id = order.Id,
                Items = itemModels.ToArray(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = _orderRepository.GetById(cart.OrderId);
                OrderViewModel model = Map(order);

                return View(model);
            }

            return View("Empty");
        }
        [HttpPost]
        public IActionResult AddItem(int id, bool inCart = true)
        {
            if (id < 0) return BadRequest("Ты чё сука, далбаёб блять? какой нахуй id " + id);

            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            var book = _bookRepository.GetById(id);

            order.AddItem(book);

            //-----------------------------

            SaveOrderAndCart(order, cart);

            if (inCart)
            {
                var model = Map(order);
                return View("Index", model);
            }
            else
            {
                return RedirectToAction("Index", "Book", new {id});
            }
        }
        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(id);

            SaveOrderAndCart(order, cart);

            var model = Map(order);
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(int id, string cellPhone, bool reset=false)
        {
            var order = _orderRepository.GetById(id);
            if (order.TotalCount == 0) return View("Empty");
            var model = Map(order);
            if (!IsValidCellPhone(cellPhone))
            {
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +380-00-000-0000";
                if (reset) return View("Confirmation", new ConfirmationViewModel { OrderId = id,
                                        CellPhone = cellPhone, Errors = new Dictionary<string, string> {
                                        { "cellPhone", "Номер телефона не соответствует формату +380-00-000-0000" }
                                        }});
                return View("Index", model);
            }
            int code = 1111; // random.Next(1000, 10000)
            HttpContext.Session.SetInt32(cellPhone, code);
            _notificationService.SendConfirmationCode(cellPhone, code);
            return View("Confirmation",
                        new ConfirmationViewModel
                        {
                            OrderId = id,
                            CellPhone = cellPhone
                        });
        }

        [HttpPost]
        public IActionResult StartDelivery(int id, string cellPhone, int code)
        {
            int? storedCode = HttpContext.Session.GetInt32(cellPhone);
            if (storedCode == null)
            {
                return View("Confirmation",
                            new ConfirmationViewModel
                            {
                                OrderId = id,
                                CellPhone = cellPhone,
                                Errors = new Dictionary<string, string>
                                {
                                    { "code", "Пустой код, повторите отправку" }
                                },
                            }); ;
            }
            if (storedCode != code)
            {
                return View("Confirmation",
                            new ConfirmationViewModel
                            {
                                OrderId = id,
                                CellPhone = cellPhone,
                                Errors = new Dictionary<string, string>
                                {
                                    { "code", "Отличается от отправленного" }
                                },
                            }); ;
            }
            cellPhone = "+380" + cellPhone;
            return View();
        }

        private bool IsValidCellPhone(string cellPhone)
        {
            if (cellPhone == null)
                return false;
            cellPhone = cellPhone.Replace(" ", "")
                                 .Replace("-", "");
            string pattern = @"^(39|50|63|66|67|68|73|93|95|96|97|98|99)\d{7}$";
            return Regex.IsMatch(cellPhone, pattern);
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            _orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;
            HttpContext.Session.Set(cart);

        }

        private (Order order, Cart cart) GetOrCreateOrderAndCart()
        {
            Order order;
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                order = _orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = _orderRepository.Create();
                cart = new Cart(order.Id);
            }

            return (order, cart);
        }
    }
}

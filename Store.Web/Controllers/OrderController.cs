using Microsoft.AspNetCore.Mvc;
using Store.Contractors;
using Store.Messages;
using Store.Web.Models;
using System.Text.RegularExpressions;
using Store.Web.Contractors;
using System.Globalization;
using Store.Web.App;

namespace Store.Web.Controllers
{
/*    public class OrderController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly INotificationService _notificationService;
        private readonly IEnumerable<IDeliveryService> _deliveryServices;
        private readonly IEnumerable<IPaymentService> _paymentServices;
        private readonly IEnumerable<IWebContractorService> _webContracts;

        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository,
                                INotificationService notificationService, IEnumerable<IDeliveryService> deliveryServices,
                                IEnumerable<IPaymentService> paymentServices, IEnumerable<IWebContractorService> webContracts)
                                
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _notificationService = notificationService;
            _deliveryServices = deliveryServices;
            _paymentServices = paymentServices;
            _webContracts = webContracts;
        }

        private OrderModel Map(Order order)
        {
            var booksIds = order.Items.Select(item => item.BookId);
            var books = _bookRepository.GetAllByIds(booksIds);
            var itemModels = from item in order.Items
                             join book in books on item.BookId equals book.Id
                             select new OrderModel
                             { 
                                 BookId = book.Id,
                                 Title = book.Title,
                                 Author = book.Author,
                                 Count = item.Count,
                                 Price = item.Price,
                             };
            return new OrderModel
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
                OrderModel model = Map(order);

                return View(model);
            }

            return View("Empty");
        }
        [HttpPost]
        public IActionResult AddItem(int id)
        {
            if (id < 0) return Json(new { success = false, message = "Ты чё сука, далбаёб блять? какой нахуй id " + id });

            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            var book = _bookRepository.GetById(id);
            if (book == null) return Json(new { success = false });

            //order.AddItem(book);
            SaveOrderAndCart(order, cart);

            var item = order.Items.FirstOrDefault(i => i.BookId == id);
            return Json(new
            {
                success = true,
                totalCount = cart.TotalCount,
                totalPrice = cart.TotalPrice.ToString("C"),
                itemCount = item?.Count ?? 0
            });
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            var item = order.Items.FirstOrDefault(i => i.BookId == id);
            if (item == null) return Json(new { success = false });

            //order.RemoveBook(id); // Припускаю, що це зменшує кількість на 1
            SaveOrderAndCart(order, cart);

            var updatedItem = order.Items.FirstOrDefault(i => i.BookId == id);
            return Json(new
            {
                success = true,
                totalCount = cart.TotalCount,
                totalPrice = cart.TotalPrice.ToString("C"),
                itemCount = updatedItem?.Count ?? 0
            });
        }

        [HttpPost]
        public IActionResult RemoveItems(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();
            var item = order.Items.FirstOrDefault(i => i.BookId == id);
            if (item == null) return Json(new { success = false });

            //order.RemoveItems(id); 
            SaveOrderAndCart(order, cart);

            var updatedItem = order.Items.FirstOrDefault(i => i.BookId == id);
            return Json(new
            {
                success = true,
                totalCount = cart.TotalCount,
                totalPrice = cart.TotalPrice.ToString("C"),
                itemCount = updatedItem?.Count ?? 0
            });
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(int id, string cellPhone, bool reset=false)
        {
            var order = _orderRepository.GetById(id);
            if (order.TotalCount == 0) return View("Empty");
            var model = Map(order);
            if (!IsValidCellPhone(cellPhone))
            {
                model.Errors["cellPhone"] = "Номер телефону не відповідає формату +380-00-000-0000";
                if (reset) return View("Confirmation", new ConfirmationViewModel { OrderId = id,
                                        CellPhone = cellPhone, Errors = new Dictionary<string, string> {
                                        { "cellPhone", "Номер телефону не відповідає формату +380-00-000-0000" }
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
        public IActionResult Confirmate(int id, string cellPhone, int code)
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
                                    { "code", "Пустий код, повторіть відправку" }
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
                                    { "code", "Відрізняється від відправленого" }
                                },
                            }); ;
            }
            cellPhone = "+380" + cellPhone;

            //todo : сохранить номер телефона

            var order = _orderRepository.GetById(id);

            order.CellPhone = cellPhone;

            _orderRepository.Update(order);

            HttpContext.Session.Remove(cellPhone);


            return RedirectToAction("StartDelivery", new { id });
        }


        public IActionResult StartDelivery(int id)
        {
            var deliveryService = _deliveryServices.First();
            var paymentService = _paymentServices.First();
            var order = _orderRepository.GetById(id);
            var deliveryForm = deliveryService.CreateForm(order.Id);
            var paymentForm = paymentService.CreateForm(order.Id);
            var model = new DeliveryDetailsViewModel
            {
                DeliveryContractors = _deliveryServices.ToDictionary(service => service.Code,
                                                        service => service.Title),
                DeliveryForm = deliveryForm,
                PaymentForm = paymentForm,
                PaymentContractors = _paymentServices.ToDictionary(service => service.Code,
                                                    service => service.Title),
            };
            return View("DeliveryForm", model);
            
            
        }
        public IActionResult UpdateDelivery(int id, string deliveryCode, string paymentCode, Dictionary<string, string> values, bool final = false)
        {
            
            var deliveryService = _deliveryServices.Single(service => service.Code == deliveryCode);
            var paymentService = _paymentServices.Single(service => service.Code == paymentCode);
            var deliveryForm = deliveryService.CreateUpdatedForm(id, values);
            var paymentForm = paymentService.CreateUpdatedForm(id, values);

            var model = new DeliveryDetailsViewModel
            {
                DeliveryContractors = _deliveryServices.ToDictionary(service => service.Code,
                                            service => service.Title),
                DeliveryForm = deliveryForm,
                PaymentForm = paymentForm,
                PaymentContractors = _paymentServices.ToDictionary(service => service.Code,
                                                    service => service.Title),
            };
            Console.WriteLine(final);
            if (final)
            {
                var order = _orderRepository.GetById(id);
                order.Payment = paymentService.CreatePayment(paymentForm);
                order.Delivery = deliveryService.CreateDelivery(deliveryForm);
                _orderRepository.Update(order);
                var webContractorService = _webContracts.SingleOrDefault(service => service.Code == paymentCode);
                if (webContractorService != null)
                {
                    return RedirectToAction("Index", "Home", new { area = webContractorService.GetUri, totalPrice = order.TotalPrice - order.Delivery.Amount });
                }
                else return View("Finish");

            }
            return View("DeliveryForm", model);
        }


        public IActionResult Finish()
        {
            HttpContext.Session.RemoveCart();
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

        
    }*/
}


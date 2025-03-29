using Microsoft.AspNetCore.Mvc;
using Store.Contractors;
using Store.Messages;
using Store.Web.Models;
using System.Text.RegularExpressions;
using Store.Web.Contractors;
using System.Globalization;
using Store.Web.App;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using NuGet.Protocol;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Store.Data.EF.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        private readonly IEnumerable<IDeliveryService> _deliveryServices;
        private readonly IEnumerable<IPaymentService> _paymentServices;
        private readonly IEnumerable<IWebContractorService> _webContracts;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderController(INotificationService notificationService, IEnumerable<IDeliveryService> deliveryServices,
                                IEnumerable<IPaymentService> paymentServices, IEnumerable<IWebContractorService> webContracts,
                                OrderService _orderService, IHttpContextAccessor httpContextAccessor)
                                
        {
            orderService = _orderService;
            _deliveryServices = deliveryServices;
            _paymentServices = paymentServices;
            _webContracts = webContracts;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (result, model) = await orderService.TryGetModelAsync();
            if (result)
            {
                return View(model);
            }
            return View("Empty");
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int id)
        {
            var order = await orderService.AddBookAsync(id);

            return Json(new
            {
                success = true,
                totalCount = order.TotalCount,
                totalPrice = order.TotalPrice.ToString("C"),
                itemCount = order.Items.GetItem(id)?.Count ?? 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var order = await orderService.RemoveBookAsync(id);

            return Json(new
            {
                success = true,
                totalCount = order.TotalCount,
                totalPrice = order.TotalPrice.ToString("C"),
                itemCount = order.Items.GetItem(id)?.Count ?? 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItems(int id)
        {
            var order = await orderService.RemoveBooksAsync(id);

            return Json(new
            {
                success = true,
                totalCount = order.TotalCount,
                totalPrice = order.TotalPrice.ToString("C"),
                itemCount = 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationCode(string cellPhone)
        {
            Console.WriteLine(cellPhone);
            var model = await orderService.SendConfirmationAsync(cellPhone);
            if (model.Errors.Count > 0)
            {
                return Json(new
                {
                    success = true,
                    errors = model.Errors
                });
            }
            else
            {
                Console.WriteLine(cellPhone);
                return Json(new
                {
                    success = false,
                    cell_phone = model.CellPhone
                });
            }
        }

        [HttpGet]
        public IActionResult Confirmation(string cellPhone, string returnUrl)
        {
            ConfirmationModel model = new ConfirmationModel();
            model.cellPhone = cellPhone;
            model.returnUrl = returnUrl;
            return View("Confirmation", model);
        }

        [HttpPost]
        public async Task<IActionResult> Confirmate(string cellPhone, int code)
        {
            var model = await orderService.ConfirmCellPhoneAsync(cellPhone, code);
            if (model.Errors.Count != 0)
            {
                return Json(new
                {
                    success = true,
                    errors = model.Errors
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> StartDelivery()
        {
            Console.WriteLine("StartDelivery");
            var deliveryService = _deliveryServices.First();
            var paymentService = _paymentServices.First();
            var order = await orderService.GetOrderAsync();
            if (order.CellPhone == null)
            {
                var userManager = _httpContextAccessor.HttpContext.RequestServices.GetService<UserManager<User>>();
                User user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user != null)
                {
                    order.CellPhone = user.PhoneNumber;
                }
                else throw new InvalidOperationException("Null phoneNumber");

            }
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

        [HttpPost]
        public async Task<IActionResult> UpdateDelivery(int id, string deliveryCode, string paymentCode,
                                                       Dictionary<string, string> values, bool final = false)
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
                var order = await orderService.GetOrderAsync();
                await orderService.SetDeliveryAsync(deliveryService.CreateDelivery(deliveryForm));
                await orderService.SetPaymentAsync(paymentService.CreatePayment(paymentForm));
                var webContractorService = _webContracts.SingleOrDefault(service => service.Code == paymentCode);
                Console.WriteLine(order.TotalPrice - order.Delivery.Amount);
                if (webContractorService != null)
                {
                    return RedirectToAction("Index", "Home", new { area = webContractorService.GetUri, totalPrice = order.TotalPrice - order.Delivery.Amount });
                }
                else
                {
                    return View("Finish");
                }
            }
            return View("DeliveryForm", model);
        }

        [HttpGet]
        public IActionResult Finish()
        {
            return View();
        }
    }

    static class ModelFeatures
    {
        public static OrderItemViewModel GetItem(this OrderItemViewModel[] model, int id)
        {
            return model.SingleOrDefault(item => item.BookId == id);
        }
    }
}


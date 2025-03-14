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

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        private readonly IEnumerable<IDeliveryService> _deliveryServices;
        private readonly IEnumerable<IPaymentService> _paymentServices;
        private readonly IEnumerable<IWebContractorService> _webContracts;

        public OrderController(INotificationService notificationService, IEnumerable<IDeliveryService> deliveryServices,
                                IEnumerable<IPaymentService> paymentServices, IEnumerable<IWebContractorService> webContracts,
                                OrderService _orderService)
                                
        {
            orderService = _orderService;
            _deliveryServices = deliveryServices;
            _paymentServices = paymentServices;
            _webContracts = webContracts;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (orderService.TryGetModel(out var Model))
                { 
                    return View(Model);
                }
            return View("Empty");
        }
        [HttpPost]
        public IActionResult AddItem(int id)
        {
            var order = orderService.AddBook(id);

            return Json(new
            {
                success = true,
                totalCount = order.TotalCount,
                totalPrice = order.TotalPrice.ToString("C"),
                itemCount = order.Items.GetItem(id)?.Count ?? 0
            });
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var order = orderService.RemoveBook(id);

            return Json(new
            {
                success = true,
                totalCount = order.TotalCount,
                totalPrice = order.TotalPrice.ToString("C"),
                itemCount = order.Items.GetItem(id)?.Count ?? 0
        });
        }

        [HttpPost]
        public IActionResult RemoveItems(int id)
        {
            var order = orderService.RemoveBooks(id);

            return Json(new
            {
                success = true,
                totalCount = order.TotalCount,
                totalPrice = order.TotalPrice.ToString("C"),
                itemCount = 0
            });
        }

        [HttpPost]
        public IActionResult SendConfirmationCode(string cellPhone)
        {
            Console.WriteLine(cellPhone);
            var model = orderService.SendConfirmation(cellPhone);
            if (model.Errors.Count > 0)
            {
                return Json(new
                {
                    success = true,
                    errors = model.Errors
                }
                );
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

        public IActionResult Confirmation(string cellPhone)
        {
            var acces = orderService.TryGetModel(out var model);
            model.CellPhone = cellPhone;
            return View(model);
        }

        [HttpPost]
        public IActionResult Confirmate(string cellPhone, int code)
        {
            var model = orderService.ConfirmCellPhone(cellPhone, code);
            if (model.Errors.Count != 0)
            {
                return Json(new
                {
                    success = true,
                    errors = model.Errors
                }
                );
            }

            else
            {
                return Json(new
                {
                    success = false,
                });
            }
        }


        public IActionResult StartDelivery()
        {
            Console.WriteLine("StartDelivery");
            var deliveryService = _deliveryServices.First();
            var paymentService = _paymentServices.First();
            var order = orderService.GetOrder();
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
                var order = orderService.GetOrder();
                orderService.SetDelivery(deliveryService.CreateDelivery(deliveryForm));
                orderService.SetPayment( paymentService.CreatePayment(paymentForm));
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


using Microsoft.AspNetCore.Mvc;
using Store.Memory;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;


        public OrderController(IBookRepository bookRepository, IOrderRepository orderRepository)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
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

        public IActionResult RemoveItem(int id)
        {
            (Order order, Cart cart) = GetOrCreateOrderAndCart();

            order.RemoveItem(id);

            SaveOrderAndCart(order, cart);

            var model = Map(order);
            return View("Index", model);
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

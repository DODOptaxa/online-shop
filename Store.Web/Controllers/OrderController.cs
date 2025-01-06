using Microsoft.AspNetCore.Mvc;
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

        public IActionResult AddItem(int id)
        {
            if (id < 0) return BadRequest("Ты чё сука, далбаёб блять? какой нахуй id " + id);

            Cart cart;
            Order order;
            if (HttpContext.Session.TryGetCart(out cart))
            {
                order = _orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = _orderRepository.Create();
                cart = new Cart(order.Id);
            }
            var book = _bookRepository.GetById(id);

            order.AddItem(book, 1);

            //-----------------------------

            _orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);

            return RedirectToAction("Index", "Book", new { id });
        }
    }
}

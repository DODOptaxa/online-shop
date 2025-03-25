using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        { 
            _bookRepository = bookRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            return View(book);
        }
    }
}

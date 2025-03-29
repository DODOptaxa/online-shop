using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.App;
using System.Threading.Tasks;
using Store.Data.EF.Identity;
using Store.Web.Models;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;

namespace Store.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService bookService;
        private readonly CommentService commentSerive;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookService bookService, CommentService commentService, IWebHostEnvironment webHostEnvironment)
        {
            this.bookService = bookService;
            this.commentSerive = commentService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(int id)
        {
            BookViewModel book = await bookService.GetById(id);
            List<CommentModel> comments = await commentSerive.GetAllByBookAsync(id);
            ViewBag.Comments = comments;
            return View(book);
        }

        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            await bookService.UploadImageAsync(id, image, _webHostEnvironment.WebRootPath);

            return RedirectToAction("Index", new { id });
        }
    }
}

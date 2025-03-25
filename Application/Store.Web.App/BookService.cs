using Store.Web.App;
using System.Collections.Generic;
using System.Linq;
using Store;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<BookViewModel> GetById(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);

            return Map(book);
        }

        public async Task<IReadOnlyCollection<BookViewModel>> GetAllByQueryAsync(string query)
        {
            var books = Book.IsIsbn(query)
                      ? await bookRepository.GetAllByIsbnAsync(query)
                      : await bookRepository.GetAllByTitleOrAuthorAsync(query);

            return books.Select(Map)
                        .ToArray();
        }

        private BookViewModel Map(Book book)
        {
            return new BookViewModel
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Price = book.Price,
            };
        }
    }
}
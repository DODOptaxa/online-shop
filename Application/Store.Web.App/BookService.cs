using Store.Web.App;
using System.Collections.Generic;
using System.Linq;
using Store;

namespace Store.Web.App
{
    public class BookService
    {
        private readonly IBookRepository bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public BookViewModel GetById(int id)
        {
            var book = bookRepository.GetById(id);

            return Map(book);
        }

        public IReadOnlyCollection<BookViewModel> GetAllByQuery(string query)
        {
            var books = Book.IsIsbn(query)
                      ? bookRepository.GetAllByIsbn(query)
                      : bookRepository.GetAllByTitleOrAuthor(query);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }
        public IEnumerable<Book> GetByQuery(string query)
        {
            if (Book.IsIsbn(query))
            {
                return _bookRepository.GetAllByISBN(query);
            }
            return _bookRepository.GetAllByTitleOrAuthor(query);
        }
    }
}

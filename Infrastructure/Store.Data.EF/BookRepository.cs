using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    internal class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetAllByIds(IEnumerable<int> bookIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllByIsbn(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllByTitleOrAuthor(string title)
        {
            throw new NotImplementedException();
        }

        public Book GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

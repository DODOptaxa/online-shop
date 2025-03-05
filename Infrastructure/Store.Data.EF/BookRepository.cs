using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store;

namespace Store.Data.EF
{
    public class BookRepository : IBookRepository
    {
        StoreDbContextFactory contextFactory;

        public BookRepository(StoreDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }
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

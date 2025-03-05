using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllByIsbn(string title);
        IEnumerable<Book> GetAllByTitleOrAuthor(string title);

        Book GetById(int id);

        IEnumerable<Book> GetAllByIds(IEnumerable<int> bookIds);
    }
}

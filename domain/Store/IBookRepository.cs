using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllByISBN(string title);
        IEnumerable<Book> GetAllByTitleOrAuthor(string title);
    }
}

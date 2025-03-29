using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllByIsbnAsync(string title);
        Task<IEnumerable<Book>> GetAllByTitleOrAuthorAsync(string title);

        Task<Book> GetByIdAsync(int id);

        Task<IEnumerable<Book>> GetAllByIdsAsync(IEnumerable<int> bookIds);

        Task<Book> GetByRandom();

        Task<IEnumerable<Book>> GetAllAsync();

        Task UpdateAsync(Book book);
    }
}

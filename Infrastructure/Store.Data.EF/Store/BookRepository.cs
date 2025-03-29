using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Store;

namespace Store.Data.EF.Store
{
    public class BookRepository : IBookRepository
    {
        StoreDbContextFactory contextFactory;

        public BookRepository(StoreDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }
        public async Task<IEnumerable<Book>> GetAllByIdsAsync(IEnumerable<int> bookIds)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            var dtos = await dbContext.Books
                                      .Where(book => bookIds.Contains(book.Id))
                                      .ToListAsync();

            return dtos.Select(Book.Mapper.Map);
        }

        public async Task<Book> GetByRandom()
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            Random rnd = new Random();

            BookDto[] dtos = await dbContext.Books.ToArrayAsync();

            var dto = dtos[rnd.Next(0, dtos.Length)];

            return Book.Mapper.Map(dto);
        }
        public async Task<IEnumerable<Book>> GetAllByIsbnAsync(string title)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            if (Book.TryFormatIsbn(title, out string formatIsbn))
            {
                var dtos = await dbContext.Books
                                          .Where(book => book.Isbn == title)
                                          .ToListAsync();

                return dtos.Select(Book.Mapper.Map);
            }

            return Array.Empty<Book>();
        }

        public async Task<IEnumerable<Book>> GetAllByTitleOrAuthorAsync(string titleOrAuthor)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            if (string.IsNullOrWhiteSpace(titleOrAuthor))
            {
                return new List<Book>();
            }

            Console.WriteLine(titleOrAuthor);
            var parameter = new SqlParameter("@titleOrAuthor", titleOrAuthor);
            var sqlQuery = $"SELECT * FROM Books WHERE CONTAINS((Author, Title), '\"*{titleOrAuthor}*\"')";
            Console.WriteLine(sqlQuery);

            var dtos = await dbContext.Books
                                      .FromSqlRaw(sqlQuery, parameter)
                                      .ToListAsync();

            return dtos.Select(Book.Mapper.Map).ToArray();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            var dto = await dbContext.Books
                                     .SingleOrDefaultAsync(book => book.Id == id);

            return Book.Mapper.Map(dto);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            var dtos = await dbContext.Books
                                      .ToListAsync();

            return dtos.Select(Book.Mapper.Map);
        }

        public async Task UpdateAsync(Book book)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            await dbContext.SaveChangesAsync();
        }
    }
}

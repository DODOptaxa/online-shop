using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
            var dbContext = contextFactory.Create(typeof(BookRepository));

            var dtos = dbContext.Books
                               .Where(book => bookIds.Contains(book.Id))
                               .ToList();

            return dtos.Select(Book.Mapper.Map);
        }

        public IEnumerable<Book> GetAllByIsbn(string title)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            if (Book.TryFormatIsbn(title, out string formatIsbn))
            {
                var dtos = dbContext.Books
                    .Where(book => book.Isbn == title)
                    .ToList();

                return dtos.Select(Book.Mapper.Map);
            }

            return Array.Empty<Book>();
        }

        public IEnumerable<Book> GetAllByTitleOrAuthor(string titleOrAuthor)
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

            var dtos = dbContext.Books
            .FromSqlRaw(sqlQuery, parameter)
            .ToArray();

            return dtos.Select(Book.Mapper.Map).ToArray();
        }

        public Book GetById(int id)
        {
            var dbContext = contextFactory.Create(typeof(BookRepository));

            var dto = dbContext.Books
                               .SingleOrDefault(book => book.Id == id);

            return Book.Mapper.Map(dto);
        }
    }
}

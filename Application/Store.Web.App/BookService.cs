using Store.Web.App;
using System.Collections.Generic;
using System.Linq;
using Store;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;

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
        public async Task<BookViewModel> GetByRandom()
        {
            var book = Map(await bookRepository.GetByRandom());
            return book;
        }

        public async Task<IReadOnlyCollection<BookViewModel>> GetAllByQueryAsync(string query)
        {
            if (query == "")
            {
                var booksWhiteSpace = await bookRepository.GetAllAsync();
                return booksWhiteSpace.Select(Map)
                                      .ToArray();
            }
            var books = Book.IsIsbn(query)
                      ? await bookRepository.GetAllByIsbnAsync(query)
                      : await bookRepository.GetAllByTitleOrAuthorAsync(query);

            return books.Select(Map)
                        .ToArray();
        }

        public async Task<BookViewModel> UploadImageAsync(int id, IFormFile image, string rootPath)
        {
            if (image != null && image.Length > 0)
            {

                string uploadsFolder = Path.Combine(rootPath, "images/books");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }


                string uniqueFileName = $"{id}_{Guid.NewGuid().ToString()}_{image.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }


                var book = await bookRepository.GetByIdAsync(id);
                book.ImagePath = $"/images/books/{uniqueFileName}";
                await bookRepository.UpdateAsync(book);

                return Map(book);
            }
            else return null;
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
                ImagePath = book.ImagePath,
            };
        }
    }
}
using Store.Data;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Store
{

    public class Book
    {
        private readonly BookDto dto;
        public int Id => dto.Id;

        public string Isbn 
        {
            get => dto.Isbn;
            set
            {
                if (TryFormatIsbn(value, out string formattedIsbn))
                    dto.Isbn = formattedIsbn;
                else throw new ArgumentException(nameof(Isbn));
            }
        }

        public string Author
        {
            get => dto.Author;
            set => dto.Author = value?.Trim();
        }

        public string Title
        {
            get => dto.Title;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(nameof(Title));

                dto.Title = value?.Trim();
            }
        }


        public string Description
        {
            get => dto.Description;
            set => dto.Description = value;
        }

        public decimal Price
        {
            get => dto.Price;
            set => dto.Price = value;
        }
        public string ImagePath
        {
            get => dto.ImagePath;
            set => dto.ImagePath = value;
        }
        internal Book(BookDto dto)
        {
            this.dto = dto;
        }

        public static bool TryFormatIsbn(string query, out string formattedIsbn)
        {
            if (query == null)
            {
                formattedIsbn = null;
                return false;
            }

            query = query.Trim();

            formattedIsbn = query.Replace("-", "")
                                .Replace(" ", "")
                                .ToUpper();

            return Regex.IsMatch(formattedIsbn, @"^ISBN\d{10}(\d{3})?$");
        }

        public static bool IsIsbn(string query) => TryFormatIsbn(query, out _);


        public static class DtoFactory
        {
            public static BookDto Create(string isbn, string author,
                                         string title, string description,
                                         decimal price)
            {
                if (TryFormatIsbn(isbn, out string formattedIsbn))
                    isbn = formattedIsbn;
                else throw new ArgumentException(nameof(isbn));

                if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(nameof (title));

                return new BookDto
                {
                    Isbn = isbn,
                    Author = author?.Trim(),
                    Title = title.Trim(),
                    Description = description?.Trim(),
                    Price = price
                };
            }
        }

        public static class Mapper
        {
            public static Book Map(BookDto dto) => new Book(dto);

            public static BookDto Map(Book domain) => domain.dto;
        }

    }

}

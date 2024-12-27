using System.Text.RegularExpressions;

namespace Store
{

    public class Book
    {
        public int Id { get; set; }

        public string Isbn { get;  }

        public string Author { get; }
        public string Title { get; }

        public Book(int id, string isbn, string author, string title)
        {
            Id = id;
            Isbn = isbn;
            Author = author;
            Title = title;

        }

        internal static bool IsIsbn(string query)
        {
            if (query == null) return false;

            query = query.Trim();

            query = query.Replace("-", "")
                                .Replace(" ", "")
                                .ToUpper();

            return Regex.IsMatch(query, @"^ISBN\d{10}(\d{3})?$");
        }
    }

}

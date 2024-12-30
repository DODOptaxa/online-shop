
namespace Store.Memory;

public class BookRepository : IBookRepository
{
    private readonly IEnumerable<Book> books = new List<Book>()
    {
        new Book(1, "ISBN 12321-67831", "А.Гитлер" , "Майн Кампф", "ПХАХАХАХААХАХАХАХ", 14.88m ),
        new Book(2, "ISBN 32141-78312", "J. Cupers", "Art of programming", "", 999m),
        new Book(3, "ISBN 31413-31489", "Billy Harringhton", "C - this is cool", "", 305m)
    };
    
    public IEnumerable<Book> GetAllByTitleOrAuthor(string title)
    {
        return books.Where(book => book.Title.ToLower().Contains(title.ToLower()) || 
                            book.Author.ToLower().Contains(title.ToLower()));
    }

    public IEnumerable<Book>? GetAllByISBN(string isbn)
    {
        return books.Where(book => book.Isbn == isbn);
    }

    public Book GetById(int id)
    {
        return books.Single(book => book.Id == id);
    }

}


namespace Store.Memory;

public class BookRepository : IBookRepository
{
    private readonly List<Book> books = new()
    {
        new Book(1, "ISBN 12321-67831", "А.Гитлер" , "Майн Кампф"),
        new Book(2, "ISBN 32141-78312", "J. Cupers", "Art of programming"),
        new Book(3, "ISBN 31413-31489", "Billy Harringhton", "C - this is cool")
    };
    
    public IEnumerable<Book> GetAllByTitleOrAuthor(string title)
    {
        return books.Where(book => book.Title.ToLower().Contains(title.ToLower()) || 
                            book.Author.ToLower().Contains(title.ToLower()));
    }

    public IEnumerable<Book> GetAllByISBN(string isbn)
    {
        return books.Where(book => book.Isbn == isbn);
    }

}

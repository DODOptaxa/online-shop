
namespace Store.Memory;

public class BookRepository : IBookRepository
{
    private readonly List<Book> books = new()
    {
        new Book(1, "Майн Кампф"),
        new Book(2, "Art of programming"),
        new Book(3, "C - this is cool")
    };
    
    public IEnumerable<Book> GetAllByTitle(string title)
    {
        return books.Where(book => book.Title.ToLower().Contains(title.ToLower()));
    }
}


namespace Store.Memory;

public class BookRepository : IBookRepository
{
    private readonly IEnumerable<Book> books = new List<Book>()
    {
        new Book(1, "ISBN 12321-67831", "А.Гитлер" , "Майн Кампф", "ПХАХАХАХААХАХАХАХ", 14.88m ),
        new Book(2, "ISBN 32141-78312", "J. Cupers", "Art of programming", "", 999m),
        new Book(3, "ISBN 31413-31489", "Billy Harringhton", "C - this is cool", "", 305m),

        new Book(4, "ISBN 45123-98765", "George Orwell", "1984", "Антиутопія про тоталітарне суспільство та контроль над розумом.", 12.99m),
        new Book(5, "ISBN 98765-43210", "J.K. Rowling", "Harry Potter and the Sorcerer's Stone", "Перша книга про пригоди юного чарівника Гаррі Поттера.", 19.95m),
        new Book(6, "ISBN 12345-67890", "F. Scott Fitzgerald", "The Great Gatsby", "Роман про американську мрію та декаданс 1920-х років.", 10.50m),
        new Book(7, "ISBN 56789-12345", "Isaac Asimov", "Foundation", "Епічна науково-фантастична сага про занепад і відродження цивілізації.", 15.75m),
        new Book(8, "ISBN 34567-89012", "Jane Austen", "Pride and Prejudice", "Класичний роман про кохання, класові упередження та сімейні цінності.", 9.99m),
        new Book(9, "ISBN 78901-23456", "Stephen King", "The Shining", "Моторошна історія про письменника в ізольованому готелі.", 22.00m),
        new Book(10, "ISBN 23456-78901", "Agatha Christie", "Murder on the Orient Express", "Детектив про загадкове вбивство у поїзді.", 14.25m),
        new Book(11, "ISBN 89012-34567", "Douglas Adams", "The Hitchhiker's Guide to the Galaxy", "Гумористична космічна пригода з абсурдними поворотами.", 13.49m),
        new Book(12, "ISBN 45678-90123", "J.R.R. Tolkien", "The Lord of the Rings", "Епічна фентезі-трилогія про боротьбу добра зі злом.", 29.99m),
        new Book(13, "ISBN 01234-56789", "Harper Lee", "To Kill a Mockingbird", "Історія про расизм і справедливість у маленькому американському містечку.", 11.80m),
        new Book(14, "ISBN 67890-12345", "Ray Bradbury", "Fahrenheit 451", "Антиутопія про суспільство, де книги під забороною.", 13.20m),
        new Book(15, "ISBN 34567-89012", "Ernest Hemingway", "The Old Man and the Sea", "Повість про боротьбу старого рибалки з природою.", 8.99m),
        new Book(16, "ISBN 90123-45678", "Terry Pratchett", "Good Omens", "Сатирична історія про ангела й демона, що рятують світ.", 17.50m),
        new Book(17, "ISBN 56789-01234", "Neil Gaiman", "American Gods", "Фентезі про старі й нові божества в сучасній Америці.", 18.75m),
        new Book(18, "ISBN 12345-67890", "Mark Twain", "The Adventures of Huckleberry Finn", "Пригоди хлопчика на Міссісіпі в епоху рабства.", 9.25m),
        new Book(19, "ISBN 78901-23456", "Aldous Huxley", "Brave New World", "Антиутопія про генетично модифіковане суспільство.", 12.60m),
        new Book(20, "ISBN 23456-78901", "Mary Shelley", "Frankenstein", "Готична розповідь про науковця та його творіння.", 10.99m),
        new Book(21, "ISBN 89012-34567", "Herman Melville", "Moby-Dick", "Епічна пригода про одержимість капітана китом.", 14.00m),
        new Book(22, "ISBN 45678-90123", "Philip K. Dick", "Do Androids Dream of Electric Sheep?", "Наукова фантастика про андроїдів і людяність.", 15.30m),
        new Book(23, "ISBN 01234-56789", "Charlotte Brontë", "Jane Eyre", "Роман про сироту, яка шукає своє місце в світі.", 11.45m),
        new Book(24, "ISBN 67890-12345", "Gabriel García Márquez", "One Hundred Years of Solitude", "Магічний реалізм про сім’ю Буендіа.", 16.80m),
        new Book(25, "ISBN 34567-89012", "Kurt Vonnegut", "Slaughterhouse-Five", "Сатира про війну та подорожі в часі.", 13.99m),
        new Book(26, "ISBN 90123-45678", "Leo Tolstoy", "War and Peace", "Епічна хроніка Наполеонівських воєн у Росії.", 25.00m),
        new Book(27, "ISBN 56789-01234", "Fyodor Dostoevsky", "Crime and Punishment", "Психологічний роман про злочин і кару.", 14.95m),
        new Book(28, "ISBN 12345-67890", "Toni Morrison", "Beloved", "Історія про травми рабства та материнську любов.", 17.25m),
        new Book(29, "ISBN 78901-23456", "William Gibson", "Neuromancer", "Кіберпанк-роман про хакерів і штучний інтелект.", 16.50m),
        new Book(30, "ISBN 23456-78901", "Ursula K. Le Guin", "The Left Hand of Darkness", "Наукова фантастика про гендер і культуру.", 15.99m),
        new Book(31, "ISBN 89012-34567", "Franz Kafka", "The Metamorphosis", "Сюрреалістична повість про перетворення людини на комаху.", 9.80m),
        new Book(32, "ISBN 45678-90123", "H.G. Wells", "The War of the Worlds", "Класична фантастика про вторгнення марсіан.", 11.99m),
        new Book(33, "ISBN 01234-56789", "Victor Hugo", "Les Misérables", "Епічна історія про справедливість і спокуту у Франції.", 22.50m)
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

    public IEnumerable<Book> GetAllByIds(IEnumerable<int> bookIds)
    {
        var foundBooks = from book in books
                         join bookId in bookIds on book.Id equals bookId
                         select book;

        return foundBooks;
    }

}

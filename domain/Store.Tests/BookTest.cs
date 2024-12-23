using Xunit;
namespace Store.Tests
{
    public class BookTest
    {
        [Fact]
        public void Test1()
        {
            bool actual = Book.IsIsbn(null);

            Assert.False(actual);
        }
    }
}
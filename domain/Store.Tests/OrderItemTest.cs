using Xunit;
namespace Store.Tests
{
    public class OrderItemTest
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            int bookId = 1;
            uint count = 5;
            decimal price = 12.99m;

            // Act
            var orderItem = new OrderItem(bookId, count, price);

            // Assert
            Assert.Equal(bookId, orderItem.BookId);
            Assert.Equal(count, orderItem.Count);
            Assert.Equal(price, orderItem.Price);
        }

        [Fact]
        public void Constructor_ShouldHandleZeroCountAndPrice()
        {
            // Arrange
            int bookId = 2;
            uint count = 0;
            
            decimal price = 0m;

            // Act
            var orderItem = new OrderItem(bookId, count, price);

            // Assert
            Assert.Equal(bookId, orderItem.BookId);
            Assert.Equal(count, orderItem.Count - 1);
            Assert.Equal(price, orderItem.Price);
        }
    }
}
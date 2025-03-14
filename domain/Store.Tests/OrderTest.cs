using Store.Data;
using Xunit;
namespace Store.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_Constructor_WithNullDto_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(null));
        }

        [Fact]
        public void Order_Id_ReturnsDtoId()
        {
            var dto = new OrderDto { Id = 1 };
            var order = new Order(dto);

            Assert.Equal(1, order.Id);
        }

        [Fact]
        public void Order_CellPhone_GetAndSet_SetsDtoValue()
        {
            var dto = new OrderDto { CellPhone = "12345" };
            var order = new Order(dto);

            order.CellPhone = "67890";
            Assert.Equal("67890", order.CellPhone);
            Assert.Equal("67890", dto.CellPhone);
        }

        [Fact]
        public void Order_CellPhone_SetNull_ThrowsArgumentException()
        {
            var dto = new OrderDto { CellPhone = "12345" };
            var order = new Order(dto);

            Assert.Throws<ArgumentException>(() => order.CellPhone = null);
        }

        [Fact]
        public void Order_Delivery_GetAndSet_SetsDtoValues()
        {
            var dto = new OrderDto();
            var order = new Order(dto);
            var delivery = new OrderDelivery("code", "desc", 10m, new Dictionary<string, string> { { "key", "value" } });

            order.Delivery = delivery;

            Assert.Equal("code", order.Delivery.UniqueCode);
            Assert.Equal("desc", order.Delivery.Description);
            Assert.Equal(10m, order.Delivery.Amount);
            Assert.Equal("value", order.Delivery.Parameters["key"]);
            Assert.Equal("code", dto.DeliveryCode);
        }

        [Fact]
        public void Order_Delivery_SetNull_ThrowsArgumentException()
        {
            var dto = new OrderDto();
            var order = new Order(dto);

            Assert.Throws<ArgumentException>(() => order.Delivery = null);
        }

        [Fact]
        public void Order_Payment_GetAndSet_SetsDtoValues()
        {
            var dto = new OrderDto();
            var order = new Order(dto);
            var payment = new OrderPayment("code", "desc", new Dictionary<string, string> { { "key", "value" } });

            order.Payment = payment;

            Assert.Equal("code", order.Payment.UniqueCode);
            Assert.Equal("desc", order.Payment.Description);
            Assert.Equal("value", order.Payment.Parameters["key"]);
            Assert.Equal("code", dto.PaymentCode);
        }

        [Fact]
        public void Order_TotalCount_ReturnsSumOfItemCounts()
        {
            var dto = new OrderDto();
            dto.Items.Add(new OrderItemDto { BookId = 1, Count = 2, Price = 10m });
            dto.Items.Add(new OrderItemDto { BookId = 2, Count = 3, Price = 20m });
            var order = new Order(dto);

            Assert.Equal(5u, order.TotalCount);
        }

        [Fact]
        public void Order_TotalPrice_ReturnsSumOfItemsPlusDelivery()
        {
            var dto = new OrderDto();
            var order = new Order(dto);

            Assert.Equal(90m, order.TotalPrice);
        }
    }
}
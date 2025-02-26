using Store.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Order
    {
        //------------------------------------

        public Order(OrderDto dto)
        {
            this.dto = dto;
            Items = new OrderItemCollection(dto);
        }

        //------------------------------------

        private readonly OrderDto dto;
        public int Id => dto.Id;

        public string CellPhone 
        { 
            get => dto.CellPhone;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(CellPhone));

                dto.CellPhone = value;
            }
        }

        public OrderDelivery Delivery 
        {
            get
            {
                if (dto.DeliveryCode == null)
                    return null;

                return new OrderDelivery(
                    dto.DeliveryCode,
                    dto.DeliveryDescription,
                    dto.DeliveryAmount,
                    dto.DeliveryParameters);
            }
            set
            {
                if (value == null)
                    throw new ArgumentException(nameof(Delivery));

                dto.DeliveryCode = value.UniqueCode;
                dto.DeliveryDescription = value.Description;
                dto.DeliveryAmount = value.Amount;
                dto.DeliveryParameters = value.Parameters.
                                                ToDictionary(p => p.Key, p => p.Value);
            }
        }

        public OrderPayment Payment
        {
            get
            {
                if (dto.PaymentCode == null)
                    return null;

                return new OrderPayment(
                    dto.PaymentCode,
                    dto.PaymentDescription,
                    dto.PaymentParameters);
            }
            set
            {
                if (value == null)
                    throw new ArgumentException(nameof(Payment));

                dto.PaymentCode = value.UniqueCode;
                dto.PaymentDescription = value.Description;
                dto.PaymentParameters = value.Parameters
                                             .ToDictionary(p => p.Key, p => p.Value);
            }
        }
        public OrderItemCollection Items { get; }

        public uint TotalCount
        {
            get { return (uint)Items.Sum(item => item.Count); }
        }

        public decimal TotalPrice
        {
            get { return Items.Sum(item => item.Price * item.Count) + (Delivery?.Amount ?? 0); }
        }

        public static class DtoFactory
        {
            public static OrderDto Create() => new OrderDto();
        }

        public static class Mapper
        {
            public static Order Map(OrderDto dto) => new Order(dto);

            public static OrderDto Map(Order domain) => domain.dto;
        }
    }
}

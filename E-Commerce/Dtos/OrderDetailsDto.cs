using Core.Entities.OrderAggregate;

namespace E_Commerce.Dtos
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public String BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ShippingAddressDto ShippedToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; }

    }
}

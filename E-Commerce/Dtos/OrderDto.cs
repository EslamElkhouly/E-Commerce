using Core.Entities.OrderAggregate;

namespace E_Commerce.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = string.Empty;
        public int DeliveryMethodId { get; set; } 
        public ShippingAddressDto address  { get; set; }   
    }
}

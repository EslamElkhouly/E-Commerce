using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterFaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync( string buyerEmail , int deliveryMethod , string basketId , ShippingAddress address );
        Task<IReadOnlyList<Order>> GetOrderForUserAsync( string buyerEmail );
        Task<Order> CreateOrderByIdAsync( int Id , string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync( );
    }
}

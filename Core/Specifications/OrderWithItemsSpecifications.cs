using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.OrderAggregate;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class OrderWithItemsSpecifications : BaseSpecification<Order>
    {
        public OrderWithItemsSpecifications(string email) : base(order => order.BuyerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderWithItemsSpecifications( int id , string email) 
            : base(order => order.Id == id && order.BuyerEmail == email)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}
 
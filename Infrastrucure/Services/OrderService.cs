using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.InterFaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, ShippingAddress address)
        {
            // Get Basket
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // Get BasketItems From  Product Repo
            var items = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);

                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);

            }

            // Get deliveryMethod
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // Calculate Subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // Payment Stuff 
            // Check to see if order exists

            var specs = new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId);
            
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecifications(specs);

            if (existingOrder != null) 
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreatOrUpdatePaymentIntent(basketId);
            }


            // Create Order
            var order = new Order(buyerEmail, address, deliveryMethod, items, subtotal,basket.PaymentIntentId);

            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();
            if (result <= 0)
                return null;


            //Delete Basket

            //await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
            => await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();

        public async Task<Order> CreateOrderByIdAsync(int id, string buyerEmail)
        {
            var orderSpecs = new OrderWithItemsSpecifications(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpecifications(orderSpecs);

        }




        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var orderSpecs = new OrderWithItemsSpecifications(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(orderSpecs);
        }
    }
}

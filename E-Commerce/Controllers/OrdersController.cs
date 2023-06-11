using AutoMapper;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using Core.InterFaces;
using E_Commerce.Dtos;
using E_Commerce.Extensions;
using E_Commerce.ResponseModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("createOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetreiveEmailFromPrincipal();

            var address = _mapper.Map<ShippingAddress>(orderDto.address);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order is null)
                return BadRequest(new ApiResponse(400, "Problem When Creating Order!!"));
            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetreiveEmailFromPrincipal();

            var order = await _orderService.CreateOrderByIdAsync(id, email);

            if (order is null)
                return NotFound(new ApiResponse(400, " Order Does not exist !!"));
            return Ok(_mapper.Map<OrderDetailsDto>(order));
        }

        [HttpGet("getAllOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrdersForUser()
        {
            var email = HttpContext.User.RetreiveEmailFromPrincipal();

            var orders = await _orderService.GetOrderForUserAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderDetailsDto>>(orders));

        }


        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
            => Ok( await _orderService.GetDeliveryMethodsAsync());

    }
}

using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.InterFaces;
using E_Commerce.ResponseModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace E_Commerce.Controllers
{

    public class PaymentsController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string whSecret = "whsec_0a083fb00db0825d08a24cabd46551147f03522903176bff1cd79280f816b653"; 
        public PaymentsController(
                                  IPaymentService paymentService,
                                  ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket =  await _paymentService.CreatOrUpdatePaymentIntent(basketId);
            if (basket == null)
                 return BadRequest(new ApiResponse(400, " Problem with your basket"));

            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                   Request.Headers["Stripe-Signature"],whSecret);

            PaymentIntent intent;

            Order order;
            
            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentPaymentFailed:
                    intent =(PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed : ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Payment Failed : ", order.Id);

                    break;

                case Events.PaymentIntentSucceeded:
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded : ", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Payment Updated to Payment Received : ", order.Id);
                    break;
            }
            return new EmptyResult();
        }

    }
}

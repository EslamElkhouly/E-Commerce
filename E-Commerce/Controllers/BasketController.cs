using AutoMapper;
using Core.Entities;
using Core.InterFaces;
using E_Commerce.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper; 
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketByID (string id)
        {
            var basket = await _basketRepository.GetBasketAsync (id);
            return Ok(basket?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket (CustomerBasketDto customerBasketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(customerBasketDto);
             var UpdatedBasket =  await _basketRepository.UpdateBasketAsync (basket);
            return Ok(UpdatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketById (string id)
            => await _basketRepository.DeleteBasketAsync (id);
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterFaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(String basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task DeleteBasketAsync(String basketId);


    }
}

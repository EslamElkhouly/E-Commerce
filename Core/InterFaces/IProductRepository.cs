using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InterFaces
{
    public interface IProductRepository : IGenericRepository<BaseEntity>
    {
        Task<Product> GetProductByIdAsync(int? id); 
        Task<IReadOnlyList<Product>> GetProductsAsync(); 
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync(); 
        Task<IReadOnlyList<ProductType>> GetProductTypeAsync(); 
      
    }
}

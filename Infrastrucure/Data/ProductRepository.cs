using Core.Entities;
using Core.InterFaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public void Add(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(ISpecificatios<BaseEntity> specifications)
        {
            throw new NotImplementedException();
        }

        public void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseEntity> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseEntity> GetEntityWithSpecifications(ISpecificatios<BaseEntity> specifications)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
            => await _context.ProductBrands.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int? id)
            => await _context.Products
            .Include(product => product.ProductType)
            .Include(product => product.ProductBrand)
            .SingleOrDefaultAsync(p => p.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
            => await _context.Products
            .Include(product => product.ProductType)
            .Include(product => product.ProductBrand)
            .ToListAsync();
            

        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
             => await _context.ProductType.ToListAsync();

        public Task<IReadOnlyList<BaseEntity>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<BaseEntity>> ListAsync(ISpecificatios<BaseEntity> specifications)
        {
            throw new NotImplementedException();
        }

        public void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecifications(ProductSpecParams productParams) 
            : base(product=>
                  (string.IsNullOrEmpty(productParams.Search)||product.Name.ToLower().Contains(productParams.Search))&&
                  (!productParams.BrandId.HasValue || product.ProductBrandId==productParams.BrandId)&&
                  (!productParams.TypeId.HasValue || product.ProductTypeId==productParams.TypeId)
                  )
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
            AddOrderBy(product => product.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Price);
                        break;
                }
            }
        }

      

        public ProductsWithTypeAndBrandSpecifications(int id)
            :base (Product=> Product.Id==id)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }
    }
}

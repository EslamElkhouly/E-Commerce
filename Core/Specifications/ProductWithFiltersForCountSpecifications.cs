﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecifications : BaseSpecification<Product>
    {
      
        public ProductWithFiltersForCountSpecifications(ProductSpecParams productParams)
      : base(product =>
            (string.IsNullOrEmpty(productParams.Search) || product.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || product.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || product.ProductTypeId == productParams.TypeId)
            )
        {

        }
    }
}

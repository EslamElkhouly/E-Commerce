using AutoMapper;
using Core.Entities;
using Core.InterFaces;
using Core.Specifications;
using E_Commerce.Dtos;
using E_Commerce.Helper;
using E_Commerce.ResponseModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
  
    public class ProductsController : BaseController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        //private readonly IProductRepository _productRepository;

        public ProductsController(/*IProductRepository productRepository*/
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper)
        {
           _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [Cached(100)]
        [HttpGet("GetProducts")]
        public async Task <ActionResult<Pagination<ProductDto>>> GetProducts([FromQuery]ProductSpecParams productSpec)
        //=>Ok (await _productRepository.ListAllAsync());
        {
            var specs = new ProductsWithTypeAndBrandSpecifications(productSpec);
            
            var countSpecs = new ProductsWithTypeAndBrandSpecifications(productSpec);
            
            var totalItems = await _productRepository.CountAsync(specs);
            
            var products = await _productRepository.ListAsync(specs);
            
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>> (products);
            
            var paginateData = new Pagination<ProductDto>(productSpec.PageIndex,productSpec.PageSize,totalItems,mappedProducts);
            
            return Ok(paginateData);
        }

        [Cached(100)]
        [HttpGet("GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var specs = new ProductsWithTypeAndBrandSpecifications(id);
            var product = await _productRepository.GetEntityWithSpecifications(specs);
            if (product is null)
                return NotFound(new ApiResponse(404));
            var mappedProducts = _mapper.Map<ProductDto>(product);

            return Ok (mappedProducts);

        }

        [Cached(100)]
        [HttpGet("GetProductBrands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
           return Ok( await _productBrandRepository.ListAllAsync());
        }

        [Cached(100)]
        [HttpGet("GetProductTypes")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}

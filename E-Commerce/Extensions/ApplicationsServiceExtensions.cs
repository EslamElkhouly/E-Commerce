using Core.InterFaces;
using E_Commerce.Helper;
using E_Commerce.ResponseModule;
using Infrastrucure.Data;
using Infrastrucure.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extensions
{
    public static class ApplicationsServiceExtensions
    {
        public static IServiceCollection AddApplicationServices ( this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBasketRepository , BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IResponseCacheService,ResponseCacheService>();

            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(e => e.Value.Errors.Count > 0)
                                .SelectMany(e => e.Value.Errors)
                                .Select(e => e.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponce
                    {
                        Errors = errors

                    };
                    return new BadRequestObjectResult(errorResponse);

                };
            });
            return services;
        }   
    }
}

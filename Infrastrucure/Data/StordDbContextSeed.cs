using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Infrastrucure.Data
{ 
    public class StordDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context , ILoggerFactory loggerFactory)
        {
            try
            {
                if(context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastrucure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                        context.ProductBrands.Add(brand);
                    await context.SaveChangesAsync();
                }



                if (context.ProductType != null && !context.ProductType.Any())
                {
                    var typesData = File.ReadAllText("../Infrastrucure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in types)
                        context.ProductType.Add(type);
                    await context.SaveChangesAsync();
                }


                if (context.Products != null && !context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastrucure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                        context.Products.Add(product);
                    await context.SaveChangesAsync();
                }

                if (context.DeliveryMethods != null && !context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Infrastrucure/Data/SeedData/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                    foreach (var method in deliveryMethods)
                        context.DeliveryMethods.Add(method);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StordDbContextSeed>();
                logger.LogError(ex.Message, "Error");
            }
        }
    }
}

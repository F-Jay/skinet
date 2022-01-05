using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrand.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    
                    foreach(var item in brands){
                        context.ProductBrand.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.ProductType.Any())
                {
                    var productTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var products = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    foreach(var product in products){
                        context.ProductType.Add(product);
                    }

                    await context.SaveChangesAsync();
                }
                
                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach(var product in products){
                        context.Products.Add(product);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.DeliveryMethods.Any()) // If we dont have any Delivery Methods - Seed Data.
                {
                    var dmData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
                    foreach(var item in methods){
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

            }catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
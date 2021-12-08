using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(); // Only can ready from list. (IReadOnlyList)
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync(); 
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
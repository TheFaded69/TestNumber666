using WebApi.Data.Entities;
using WebApi.Models;

namespace WebApi.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync(bool? isSold = null);
    Task<Product> GetProductAsync(int id);
    Task<Product> SaveProductAsync(SaveProductInput input);
    Task<bool> DeleteProductAsync(int id);
}

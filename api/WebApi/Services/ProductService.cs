using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Entities;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Product>> GetProductsAsync()
    {
        return _dbContext.Products.AsNoTracking().Where(x => x.DeletedAt == null).OrderByDescending(x => x.Id).ToListAsync();
    }

    public Task<Product> GetProductAsync(int id)
    {
        return _dbContext.Products.AsNoTracking().FirstAsync(x => x.Id == id);
    }

    public async Task<Product> SaveProductAsync(SaveProductInput input)
    {
        var product = default(Product);
        if (input.Id > 0)
        {
            product = await _dbContext.Products.Where(x => x.Id == input.Id && x.DeletedAt == null).FirstOrDefaultAsync();
        }
        else
        {
            product = new Product() { CreatedAt = DateTime.UtcNow };
            _dbContext.Products.Add(product);
        }
        if (product != null)
        {
            product.UpdatedAt = DateTime.UtcNow;
            product.Title = input.Title;
            product.Description = input.Description;
            product.IsSold = input.IsSold;

            await _dbContext.SaveChangesAsync();
        }
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _dbContext.Products.Where(x => x.Id == id && x.DeletedAt == null).FirstOrDefaultAsync();
        if (product == null) return false;
        product.DeletedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}

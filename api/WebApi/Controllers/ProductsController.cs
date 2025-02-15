using Microsoft.AspNetCore.Mvc;
using WebApi.Data.Entities;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public Task<List<Product>> Get()
    {
        return _productService.GetProductsAsync();
    }

    [HttpGet("{id}")]
    public Task<Product> Get(int id)
    {
        return _productService.GetProductAsync(id);
    }

    [HttpPost]
    public Task<Product> Save([FromBody] SaveProductInput input)
    {
        return _productService.SaveProductAsync(input);
    }

    [HttpDelete("{id}")]
    public Task<bool> Delete(int id)
    {
        return _productService.DeleteProductAsync(id);
    }
}

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
    public async Task<ActionResult<List<Product>>> Get([FromQuery] bool? isSold = null)
    {
        var products = await _productService.GetProductsAsync(isSold);

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(int id)
    {
        var product = await _productService.GetProductAsync(id);

        return product == null ? NotFound() : Ok(product);
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

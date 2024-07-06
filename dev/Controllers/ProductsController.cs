using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dev.Models;
using dev.Services;
using dev.ViewModels;

namespace dev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> CreateProduct(ProductViewModel productViewModel)
        {
            var createdProduct = await _productService.CreateProductAsync(productViewModel);
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductViewModel>> UpdateProduct(int productId, ProductViewModel productViewModel)
        {
            var updatedProduct = await _productService.UpdateProductAsync(productId, productViewModel);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dev.Models;

namespace dev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> Products = new List<Product>();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(Products);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            product.Id = Products.Count > 0 ? Products.Max(p => p.Id) + 1 : 1;
            Products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { productId = product.Id }, product);
        }

        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductById(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public ActionResult<Product> UpdateProduct(int productId, Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;

            return Ok(product);
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(int productId)
        {
            var product = Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            Products.Remove(product);
            return NoContent();
        }
    }
}

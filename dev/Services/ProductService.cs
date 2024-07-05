using dev.Data;
using dev.Models;
using dev.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dev.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .ToListAsync();

            return products;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel)
        {
            var newProduct = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            productViewModel.Id = newProduct.Id;

            return productViewModel;
        }

        public async Task<ProductViewModel> UpdateProductAsync(int productId, ProductViewModel productViewModel)
        {
            var existingProduct = await _context.Products.FindAsync(productId);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productViewModel.Name;
            existingProduct.Description = productViewModel.Description;
            existingProduct.Price = productViewModel.Price;

            await _context.SaveChangesAsync();

            return productViewModel;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

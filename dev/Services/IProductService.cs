using dev.ViewModels;

namespace dev.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(int productId);
        Task<ProductViewModel> CreateProductAsync(ProductViewModel productViewModel);
        Task<ProductViewModel> UpdateProductAsync(int productId, ProductViewModel productViewModel);
        Task<bool> DeleteProductAsync(int productId);
    }
}

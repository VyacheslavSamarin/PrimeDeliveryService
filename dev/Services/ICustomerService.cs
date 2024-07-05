using dev.ViewModels;

namespace dev.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync();
        Task<CustomerViewModel> GetCustomerByIdAsync(int customerId);
        Task<CustomerViewModel> CreateCustomerAsync(CustomerViewModel customerViewModel);
        Task<CustomerViewModel> UpdateCustomerAsync(int customerId, CustomerViewModel customerViewModel);
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}

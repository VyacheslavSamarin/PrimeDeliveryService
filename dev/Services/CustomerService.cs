using dev.Data;
using dev.Models;
using dev.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dev.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .ToListAsync();

            return customers;
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == customerId)
                .Select(c => new CustomerViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<CustomerViewModel> CreateCustomerAsync(CustomerViewModel customerViewModel)
        {
            var newCustomer = new Customer
            {
                Name = customerViewModel.Name,
                Address = customerViewModel.Address,
                Email = customerViewModel.Email,
                Phone = customerViewModel.Phone
            };

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            customerViewModel.Id = newCustomer.Id; // Обновляем Id созданного клиента

            return customerViewModel;
        }

        public async Task<CustomerViewModel> UpdateCustomerAsync(int customerId, CustomerViewModel customerViewModel)
        {
            var existingCustomer = await _context.Customers.FindAsync(customerId);

            if (existingCustomer == null)
            {
                // В данном случае можно выбрасывать исключение или возвращать null / пустой объект в зависимости от требований
                return null;
            }

            existingCustomer.Name = customerViewModel.Name;
            existingCustomer.Address = customerViewModel.Address;
            existingCustomer.Email = customerViewModel.Email;
            existingCustomer.Phone = customerViewModel.Phone;

            await _context.SaveChangesAsync();

            return customerViewModel;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                // В данном случае можно выбрасывать исключение или возвращать false в зависимости от требований
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

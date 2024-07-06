using AutoMapper;
using dev.Data;
using dev.Models;
using dev.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace dev.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerViewModel>>(customers);
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            return _mapper.Map<CustomerViewModel>(customer);
        }

        public async Task<CustomerViewModel> CreateCustomerAsync(CustomerViewModel customerViewModel)
        {
            ValidateCustomer(customerViewModel);

            var newCustomer = _mapper.Map<Customer>(customerViewModel);
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            customerViewModel.Id = newCustomer.Id;
            return customerViewModel;
        }

        public async Task<CustomerViewModel> UpdateCustomerAsync(int customerId, CustomerViewModel customerViewModel)
        {
            ValidateCustomer(customerViewModel);

            var existingCustomer = await _context.Customers.FindAsync(customerId);

            if (existingCustomer == null)
            {
                return null;
            }

            _mapper.Map(customerViewModel, existingCustomer);
            await _context.SaveChangesAsync();

            return customerViewModel;
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }

        private void ValidateCustomer(CustomerViewModel customerViewModel)
        {
            var validationContext = new ValidationContext(customerViewModel, null, null);
            Validator.ValidateObject(customerViewModel, validationContext, validateAllProperties: true);
        }
    }
}

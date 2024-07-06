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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> CreateCustomer(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCustomer = await _customerService.CreateCustomerAsync(customerViewModel);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = createdCustomer.Id }, createdCustomer);
        }

        [HttpPut("{customerId}")]
        public async Task<ActionResult<CustomerViewModel>> UpdateCustomer(int customerId, CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedCustomer = await _customerService.UpdateCustomerAsync(customerId, customerViewModel);
            if (updatedCustomer == null)
            {
                return NotFound();
            }

            return Ok(updatedCustomer);
        }

        [HttpDelete("{customerId}")]
        public async Task<ActionResult> DeleteCustomer(int customerId)
        {
            var deleted = await _customerService.DeleteCustomerAsync(customerId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

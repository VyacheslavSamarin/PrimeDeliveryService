using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dev.Models;

namespace dev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private static List<Customer> Customers = new List<Customer>();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(Customers);
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            Customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { customerId = customer.Id }, customer);
        }

        [HttpGet("{customerId}")]
        public ActionResult<Customer> GetCustomerById(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPut("{customerId}")]
        public ActionResult<Customer> UpdateCustomer(int customerId, Customer updatedCustomer)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = updatedCustomer.Name;
            customer.Address = updatedCustomer.Address;
            customer.Email = updatedCustomer.Email;
            customer.Phone = updatedCustomer.Phone;

            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public ActionResult DeleteCustomer(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }

            Customers.Remove(customer);
            return NoContent();
        }
    }
}

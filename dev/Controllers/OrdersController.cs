using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dev.Models;

namespace dev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static List<Order> Orders = new List<Order>();

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            return Ok(Orders);
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            order.OrderId = Orders.Count > 0 ? Orders.Max(o => o.OrderId) + 1 : 1;
            Orders.Add(order);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
        }

        [HttpGet("{orderId}")]
        public ActionResult<Order> GetOrderById(int orderId)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut("{orderId}")]
        public ActionResult<Order> UpdateOrder(int orderId, Order updatedOrder)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.CustomerId = updatedOrder.CustomerId;
            order.Products = updatedOrder.Products;
            order.OrderStatus = updatedOrder.OrderStatus;
            order.OrderDate = updatedOrder.OrderDate;
            order.DeliveryDate = updatedOrder.DeliveryDate;
            order.Address = updatedOrder.Address;

            return Ok(order);
        }

        [HttpDelete("{orderId}")]
        public ActionResult DeleteOrder(int orderId)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            Orders.Remove(order);
            return NoContent();
        }

        [HttpPut("status")]
        public ActionResult UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == request.OrderId);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = request.OrderStatus;
            return Ok();
        }
    }
}

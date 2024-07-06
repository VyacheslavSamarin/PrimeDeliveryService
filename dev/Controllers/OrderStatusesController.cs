using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using dev.Models;

namespace dev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusesController : ControllerBase
    {
        private static List<OrderStatus> OrderStatuses = new List<OrderStatus>();

        [HttpGet]
        public ActionResult<IEnumerable<OrderStatus>> GetAllOrderStatuses()
        {
            return Ok(OrderStatuses);
        }

        [HttpPost]
        public ActionResult<OrderStatus> CreateOrderStatus(OrderStatus orderStatus)
        {
            orderStatus.Id = OrderStatuses.Count > 0 ? OrderStatuses.Max(os => os.Id) + 1 : 1;
            OrderStatuses.Add(orderStatus);
            return CreatedAtAction(nameof(GetOrderStatusById), new { orderStatusId = orderStatus.Id }, orderStatus);
        }

        [HttpGet("{orderStatusId}")]
        public ActionResult<OrderStatus> GetOrderStatusById(int orderStatusId)
        {
            var orderStatus = OrderStatuses.FirstOrDefault(os => os.Id == orderStatusId);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return Ok(orderStatus);
        }

        [HttpPut("{orderStatusId}")]
        public ActionResult<OrderStatus> UpdateOrderStatus(int orderStatusId, OrderStatus updatedOrderStatus)
        {
            var orderStatus = OrderStatuses.FirstOrDefault(os => os.Id == orderStatusId);
            if (orderStatus == null)
            {
                return NotFound();
            }

            orderStatus.Name = updatedOrderStatus.Name;

            return Ok(orderStatus);
        }

        [HttpDelete("{orderStatusId}")]
        public ActionResult DeleteOrderStatus(int orderStatusId)
        {
            var orderStatus = OrderStatuses.FirstOrDefault(os => os.Id == orderStatusId);
            if (orderStatus == null)
            {
                return NotFound();
            }

            OrderStatuses.Remove(orderStatus);
            return NoContent();
        }
    }
}

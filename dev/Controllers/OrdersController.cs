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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderViewModel>> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> CreateOrder(OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdOrder = await _orderService.CreateOrderAsync(orderViewModel);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult<OrderViewModel>> UpdateOrder(int orderId, OrderViewModel orderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOrder = await _orderService.UpdateOrderAsync(orderId, orderViewModel);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            var deleted = await _orderService.DeleteOrderAsync(orderId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("status")]
        public async Task<ActionResult> UpdateOrderStatus(UpdateOrderStatusRequestViewModel statusViewModel)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(statusViewModel.Id, statusViewModel);
            if (!updated)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

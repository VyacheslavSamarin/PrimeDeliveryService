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
    public class OrderStatusesController : ControllerBase
    {
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusesController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusViewModel>>> GetAllOrderStatuses()
        {
            var orderStatuses = await _orderStatusService.GetAllOrderStatusesAsync();
            return Ok(orderStatuses);
        }

        [HttpGet("{orderStatusId}")]
        public async Task<ActionResult<OrderStatusViewModel>> GetOrderStatusById(int orderStatusId)
        {
            var orderStatus = await _orderStatusService.GetOrderStatusByIdAsync(orderStatusId);
            if (orderStatus == null)
            {
                return NotFound();
            }
            return Ok(orderStatus);
        }

        [HttpPost]
        public async Task<ActionResult<OrderStatusViewModel>> CreateOrderStatus(OrderStatusViewModel orderStatusViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdOrderStatus = await _orderStatusService.CreateOrderStatusAsync(orderStatusViewModel);
            return CreatedAtAction(nameof(GetOrderStatusById), new { orderStatusId = createdOrderStatus.Id }, createdOrderStatus);
        }

        [HttpPut("{orderStatusId}")]
        public async Task<ActionResult<OrderStatusViewModel>> UpdateOrderStatus(int orderStatusId, OrderStatusViewModel orderStatusViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOrderStatus = await _orderStatusService.UpdateOrderStatusAsync(orderStatusId, orderStatusViewModel);
            if (updatedOrderStatus == null)
            {
                return NotFound();
            }

            return Ok(updatedOrderStatus);
        }

        [HttpDelete("{orderStatusId}")]
        public async Task<ActionResult> DeleteOrderStatus(int orderStatusId)
        {
            var deleted = await _orderStatusService.DeleteOrderStatusAsync(orderStatusId);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

using dev.Data;
using dev.Models;
using dev.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dev.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly AppDbContext _context;

        public OrderStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderStatusViewModel>> GetAllOrderStatusesAsync()
        {
            var orderStatuses = await _context.OrderStatuses
                .Select(os => new OrderStatusViewModel
                {
                    Id = os.Id,
                    Name = os.Name
                })
                .ToListAsync();

            return orderStatuses;
        }

        public async Task<OrderStatusViewModel> GetOrderStatusByIdAsync(int orderStatusId)
        {
            var orderStatus = await _context.OrderStatuses
                .Where(os => os.Id == orderStatusId)
                .Select(os => new OrderStatusViewModel
                {
                    Id = os.Id,
                    Name = os.Name
                })
                .FirstOrDefaultAsync();

            return orderStatus;
        }

        public async Task<OrderStatusViewModel> CreateOrderStatusAsync(OrderStatusViewModel orderStatusViewModel)
        {
            var newOrderStatus = new OrderStatus
            {
                Name = orderStatusViewModel.Name
            };

            _context.OrderStatuses.Add(newOrderStatus);
            await _context.SaveChangesAsync();

            orderStatusViewModel.Id = newOrderStatus.Id; // Обновляем Id созданного статуса заказа

            return orderStatusViewModel;
        }

        public async Task<OrderStatusViewModel> UpdateOrderStatusAsync(int orderStatusId, OrderStatusViewModel orderStatusViewModel)
        {
            var existingOrderStatus = await _context.OrderStatuses.FindAsync(orderStatusId);

            if (existingOrderStatus == null)
            {
                // В данном случае можно выбрасывать исключение или возвращать null / пустой объект в зависимости от требований
                return null;
            }

            existingOrderStatus.Name = orderStatusViewModel.Name;

            await _context.SaveChangesAsync();

            return orderStatusViewModel;
        }

        public async Task<bool> DeleteOrderStatusAsync(int orderStatusId)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(orderStatusId);

            if (orderStatus == null)
            {
                // В данном случае можно выбрасывать исключение или возвращать false в зависимости от требований
                return false;
            }

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

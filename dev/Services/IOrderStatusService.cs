using dev.ViewModels;

namespace dev.Services
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatusViewModel>> GetAllOrderStatusesAsync();
        Task<OrderStatusViewModel> GetOrderStatusByIdAsync(int orderStatusId);
        Task<OrderStatusViewModel> CreateOrderStatusAsync(OrderStatusViewModel orderStatusViewModel);
        Task<OrderStatusViewModel> UpdateOrderStatusAsync(int orderStatusId, OrderStatusViewModel orderStatusViewModel);
        Task<bool> DeleteOrderStatusAsync(int orderStatusId);
    }
}

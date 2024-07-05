using dev.ViewModels;

namespace dev.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
        Task<OrderViewModel> GetOrderByIdAsync(int orderId);
        Task<OrderViewModel> CreateOrderAsync(OrderViewModel orderViewModel);
        Task<OrderViewModel> UpdateOrderAsync(int orderId, OrderViewModel orderViewModel);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<bool> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusRequestViewModel statusViewModel);
    }
}

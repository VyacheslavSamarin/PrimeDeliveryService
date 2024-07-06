using dev.Data;
using dev.Models;
using dev.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace dev.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.Products)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    Products = o.Products.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList(),
                    OrderStatus = o.OrderStatus,
                    OrderDate = o.OrderDate,
                    DeliveryDate = o.DeliveryDate,
                    Address = o.Address
                })
                .ToListAsync();

            return orders;
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Products)
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    Products = o.Products.Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price
                    }).ToList(),
                    OrderStatus = o.OrderStatus,
                    OrderDate = o.OrderDate,
                    DeliveryDate = o.DeliveryDate,
                    Address = o.Address
                })
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<OrderViewModel> CreateOrderAsync(OrderViewModel orderViewModel)
        {
            var newOrder = new Order
            {
                CustomerId = orderViewModel.CustomerId,
                Products = orderViewModel.Products.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToList(),
                OrderStatus = orderViewModel.OrderStatus,
                OrderDate = orderViewModel.OrderDate,
                DeliveryDate = orderViewModel.DeliveryDate,
                Address = orderViewModel.Address
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            orderViewModel.OrderId = newOrder.OrderId;

            return orderViewModel;
        }

        public async Task<OrderViewModel> UpdateOrderAsync(int orderId, OrderViewModel orderViewModel)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.CustomerId = orderViewModel.CustomerId;
            existingOrder.Products = orderViewModel.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();
            existingOrder.OrderStatus = orderViewModel.OrderStatus;
            existingOrder.OrderDate = orderViewModel.OrderDate;
            existingOrder.DeliveryDate = orderViewModel.DeliveryDate;
            existingOrder.Address = orderViewModel.Address;

            await _context.SaveChangesAsync();

            return orderViewModel;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusRequestViewModel statusViewModel)
        {
            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                return false;
            }

            order.OrderStatus = statusViewModel.Name;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

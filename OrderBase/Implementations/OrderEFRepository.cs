using Microsoft.EntityFrameworkCore;
using OrderBase.DBContext;
using OrderBase.Entities;
using OrderBase.Interfaces;

namespace OrderBase.Implementations
{
    public class OrderEFRepository : IOrderRepository
    {
        private readonly Context _context;
        
        public OrderEFRepository(Context context) { 
        _context = context;
        }

        public async Task AddOrder(Order order)
        {
            order.CreatedOn = DateTime.UtcNow;
            await _context.Orders.AddAsync(order);
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<List<Order>> GetOrdersByCustomerId(Guid customerId , bool isDeleted)
        {
            List<Order> orders = await _context.Orders.Where(order => order.CustomerId == customerId && order.IsDeleted == isDeleted)
                                                      .Include(c => c.OrderItems).ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderById(Guid id)
        {
           var order = await _context.Orders.FindAsync(id);
           return order; 
        }
    }
}

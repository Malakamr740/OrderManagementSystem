using OrderBase.Entities;

namespace OrderBase.Interfaces
{
    public interface IOrderRepository
    {
        public Task AddOrder(Order order);
        public void DeleteOrder(Order order);
        public Task<List<Order>> GetOrdersByCustomerId(Guid customerId , bool isDeleted);
        public Task<Order> GetOrderById(Guid id);
    }
}

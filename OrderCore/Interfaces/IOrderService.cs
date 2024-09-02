using OrderCore.DTO.Request;
using OrderCore.DTO.Response;

namespace OrderCore.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderResponseDTO> AddOrder(OrderRequestDTO order);
        public Task<bool> DeleteOrder(Guid id);
        public Task<List<OrderRequestDTO>> GetOrdersByCustomerId(Guid id);
    }
}

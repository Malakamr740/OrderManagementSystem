using Mapster;
using OrderBase.Entities;
using OrderBase.Interfaces;
using OrderCore.DTO.Request;
using OrderCore.DTO.Response;
using OrderCore.Interfaces;


namespace OrderCore.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private const float tax = 0.14f;
        public OrderService(IOrderRepository orderRepository,IUnitOfWork unitOfWork,IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }
        
        public async Task<OrderResponseDTO> AddOrder(OrderRequestDTO addedOrder)
        {
            addedOrder.Amount = await CalculateCost(addedOrder.OrderItems);
            addedOrder.Tax = tax;
            addedOrder.TotalAmount = CalculateTotalCost(addedOrder.Amount);
            Order order = addedOrder.Adapt<Order>();
            await _orderRepository.AddOrder(order);
            var orderResponseDTO = order.Adapt<OrderResponseDTO>();
            bool isSaved = Convert.ToBoolean(await _unitOfWork.SaveChangesAsync());
            var result = isSaved == true ? orderResponseDTO : null;
            return result;
        }


        private double CalculateTotalCost(double amount) 
        {
            Double totalAmount = amount + (tax  * amount);
            return Math.Round(totalAmount,2);
        }

        private async Task<double> CalculateCost(ICollection<OrderItemDTO> orderItems)
        {
            Double total = 0;
            foreach (OrderItemDTO item in orderItems)
            {
                Product product = await _productRepository.GetProductById(item.ProductId);
                product.Quantity-=item.Quantity;
                item.Cost = (product.Amount * item.Quantity); 
                total += item.Cost;
            }
            return total;
        }


        public async Task<bool> DeleteOrder(Guid id)
        {
            var order = await _orderRepository.GetOrderById(id);
            order.IsDeleted = !(order.IsDeleted);
            _orderRepository.DeleteOrder(order);
            var result = Convert.ToBoolean(await _unitOfWork.SaveChangesAsync());
            return result;       
        }

        public async Task<List<OrderRequestDTO>> GetOrdersByCustomerId(Guid id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer != null)
            {
                var isDeleted = false;
                var allOrders = await _orderRepository.GetOrdersByCustomerId(id, isDeleted);
                var orderDTOs = allOrders.Adapt<List<OrderRequestDTO>>();
                return orderDTOs;
            }
            else
            {
                return null;
            }
        }

       
    }
}

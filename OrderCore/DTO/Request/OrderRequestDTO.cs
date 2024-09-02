namespace OrderCore.DTO.Request
{
    public class OrderRequestDTO : OrderDTO
    {

        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}

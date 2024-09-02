using OrderCore.DTO.Request;

namespace OrderCore.DTO.Response
{
    public class OrderResponseDTO : OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public ICollection<OrderItemResponseDTO> OrderItems { get; set; }

    }
}

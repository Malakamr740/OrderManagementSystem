namespace OrderCore.DTO.Request
{
    public class OrderItemDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Double Cost { get; set; }
    }
}

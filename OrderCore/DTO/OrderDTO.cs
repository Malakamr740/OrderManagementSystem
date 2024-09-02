namespace OrderCore.DTO
{
    public class OrderDTO
    {
        public double Amount { get; set; }
        public float Tax { get; set; }
        public double TotalAmount { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CustomerId { get; set; }
    }
}

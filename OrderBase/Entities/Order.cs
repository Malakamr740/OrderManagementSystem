namespace OrderBase.Entities
{
    public class Order
    {
        public Guid Id{ get; set; }
        public Double Amount { get; set; }
        public float Tax { get; set; }
        public Double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Customer? Customer { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}

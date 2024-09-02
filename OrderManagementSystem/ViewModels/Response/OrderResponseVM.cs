using OrderManagementSystem.ViewModels.Request;

namespace OrderManagementSystem.ViewModels.Response
{
    public class OrderResponseVM : OrderVM
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public float Tax { get; set; }
        public double TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<OrderItemResponseVM> OrderItems { get; set; } = new List<OrderItemResponseVM>();

    }
}

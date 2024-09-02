namespace OrderManagementSystem.ViewModels.Request
{
    public class OrderRequestVM : OrderVM
    {
        public ICollection<OrderItemVM> OrderItems { get; set; } = new List<OrderItemVM>();

    }
}

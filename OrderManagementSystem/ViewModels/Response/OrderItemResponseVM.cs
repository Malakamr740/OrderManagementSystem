using OrderManagementSystem.ViewModels.Request;

namespace OrderManagementSystem.ViewModels.Response
{
    public class OrderItemResponseVM : OrderItemVM
    {
        public Guid Id { get; set; }
        public Double Cost { get; set; }
    }
}

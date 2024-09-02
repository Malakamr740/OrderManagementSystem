using OrderManagementSystem.ViewModels.Request;

namespace OrderManagementSystem.ViewModels.Response
{
    public class CustomerResponseVM : CustomerVM
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}

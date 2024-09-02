using OrderManagementSystem.ViewModels.Request;

namespace OrderManagementSystem.ViewModels
{
    public class ProductResponseVM : ProductVM
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }



    }
}

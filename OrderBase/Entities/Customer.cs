using System.ComponentModel.DataAnnotations;

namespace OrderBase.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Status { get; set; } = String.Empty;  
        public string PasswordHash { get; set; } = String.Empty;
        public string PasswordSalt { get; set; } = String.Empty;
        public bool IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

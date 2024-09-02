using OrderCore.DTO.Request;

namespace OrderCore.DTO.Response
{
    public class CustomerResponseDTO : CustomerDTO
    {
        public Guid Id { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}

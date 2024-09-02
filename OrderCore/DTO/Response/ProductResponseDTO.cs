using OrderCore.DTO.Request;

namespace OrderCore.DTO.Response
{
    public class ProductResponseDTO : ProductDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}

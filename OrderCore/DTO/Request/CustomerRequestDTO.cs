namespace OrderCore.DTO.Request
{
    public class CustomerRequestDTO : CustomerDTO
    {
        public string Password { get; set; } = string.Empty;
    }
}

using OrderCore.DTO.Request;
using OrderCore.DTO.Response;

namespace OrderCore.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerResponseDTO> ValidateEmail(string email);
        public bool ValidatePassword(string password, string PasswordHash, string PasswordSalt);
        public Task<CustomerResponseDTO> Register(CustomerRequestDTO customer);
 
    }
}

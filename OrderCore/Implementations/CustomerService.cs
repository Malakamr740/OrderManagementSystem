using Mapster;
using OrderBase.Entities;
using OrderBase.Interfaces;
using OrderCore.DTO.Request;
using OrderCore.DTO.Response;
using OrderCore.Interfaces;

namespace OrderCore.Implementations
{
    public class CustomerService :  ICustomerService
    {            
        private readonly ICustomerRepository _customerEFRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHasher _hasher;

        public CustomerService(ICustomerRepository customerEFRepository , IUnitOfWork unitOfWork , IHasher hasher) { 
            _customerEFRepository = customerEFRepository;
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        public async Task<CustomerResponseDTO> GetCustomerById(Guid id)
        {
            var customer = await _customerEFRepository.GetCustomerById(id);
            var customerResponseDTO = customer.Adapt<CustomerResponseDTO>();
            return customerResponseDTO;
        }

        public async Task<CustomerResponseDTO> Register(CustomerRequestDTO addedCustomer)
        {
            var customerEntity = addedCustomer.Adapt<Customer>();
            var customer = await _customerEFRepository.GetCustomerByEmail(addedCustomer.Email);
            if (customer == null)
            {
                (customerEntity.PasswordSalt , customerEntity.PasswordHash) = _hasher.GenerateHash(addedCustomer.Password);
                await _customerEFRepository.AddCustomer(customerEntity);
                await _unitOfWork.SaveChangesAsync();
                var customerResponseDTO =customerEntity.Adapt<CustomerResponseDTO>();
                return customerResponseDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomerResponseDTO> ValidateEmail(string email)
        {
            var customer = await _customerEFRepository.GetCustomerByEmail(email);
            var customerDTO = customer.Adapt<CustomerResponseDTO>();
            return customerDTO;

        }

        public bool ValidatePassword(string password ,string PasswordHash, string PasswordSalt)
        {
            var result = _hasher.VerifyPassword(password, PasswordHash, PasswordSalt);
            return result;

        }
    }
}

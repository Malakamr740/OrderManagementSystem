using OrderBase.Entities;
namespace OrderBase.Interfaces
{
    public interface ICustomerRepository
    {
        public Task AddCustomer(Customer customer);
        public Task<Customer> GetCustomerByEmailAndPassword(string email , string password);
        public Task<Customer> GetCustomerByEmail(string email);
        public Task<Customer> GetCustomerById(Guid id);
        
    }
}

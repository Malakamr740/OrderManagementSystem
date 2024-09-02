using Microsoft.EntityFrameworkCore;
using OrderBase.DBContext;
using OrderBase.Entities;
using OrderBase.Interfaces;

namespace OrderBase.Implementations
{
    public class CustomerEFRepository : ICustomerRepository
    {
        private readonly Context _context;
        private readonly IHasher _hasher;
        public CustomerEFRepository(Context context , IHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }


        public async Task AddCustomer(Customer customer) 
        {
             await _context.Customer.AddAsync(customer);            
        }
         

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(customer=>customer.Email== email);
            return customer;
        }

        public async Task<Customer> GetCustomerByEmailAndPassword(string email, string password)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(customer => customer.Email == email && customer.PasswordSalt == password);
            return customer;
        }

        public async Task<Customer> GetCustomerById(Guid id)
        {
            Customer customer = await _context.Customer.FirstOrDefaultAsync(cid => cid.Id == id);
            return customer;
        }
    }
}

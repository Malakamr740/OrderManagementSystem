using OrderBase.DBContext;
using OrderBase.Entities;
using OrderBase.Interfaces;

namespace OrderBase.Implementations
{
    public class CustomerSeeder : ISeeder 
    {
        private readonly Context _context;
        private readonly IHasher _hasher;
        public CustomerSeeder(Context context, IHasher hasher) { 

            _context = context;
            _hasher = hasher;

        }

        public void Seed()
        {
            if (!(_context.Customer.Any()))
            {
                Customer customer = new Customer()
                {
                    Name = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    Phone = "2001098765543",
                    Status = "Active",
                    Address = "SuperAdmin",
                    PasswordHash = "12345",
                    IsAdmin = true,
                    IsDeleted = false
                    
                };
                (customer.PasswordSalt , customer.PasswordHash) = _hasher.GenerateHash(customer.PasswordHash);
                _context.Customer.Add(customer);
                _context.SaveChanges();
            }
        }
    }
}

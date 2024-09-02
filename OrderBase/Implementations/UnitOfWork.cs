using OrderBase.Interfaces;
using OrderBase.DBContext;

namespace OrderBase.Implementations
{
    public class UnitOfWork:IUnitOfWork
    {

        private readonly Context context;
        public UnitOfWork(Context _context)
        {
            context = _context;
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await context.SaveChangesAsync();
            return result;
        }

    }
}

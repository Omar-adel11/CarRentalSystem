using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.BLL.Interfaces;
using CarRentalSystem.DAL.Data.Contexts;

namespace CarRentalSystem.BLL.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarDbContexts _context;

        public UnitOfWork(CarDbContexts context) {
            _context = context;
            carRepo = new CarRepo(context);
        }
        public ICarRepo carRepo{ get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync();
        }
    }
}

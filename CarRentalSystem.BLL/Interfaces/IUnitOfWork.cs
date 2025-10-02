using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICarRepo carRepo { get; }

        Task<int> CompleteAsync();
    }
}

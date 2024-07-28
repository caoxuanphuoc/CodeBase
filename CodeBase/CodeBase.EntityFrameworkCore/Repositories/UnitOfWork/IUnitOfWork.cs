using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.EntityFrameworkCore.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        Task SaveChangesAsync();
    }
}

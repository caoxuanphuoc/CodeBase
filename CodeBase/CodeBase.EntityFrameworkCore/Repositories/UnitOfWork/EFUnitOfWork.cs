using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.EntityFrameworkCore.Repositories.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly CodebBaseDbContext _context;
        public EFUnitOfWork(CodebBaseDbContext context)
        {
            _context = context;
        }   
        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

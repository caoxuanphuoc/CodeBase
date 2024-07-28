using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.EntityFrameworkCore.Repositories
{
    public interface IBaseRepository<TEntity, in TKey>
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id);
        IQueryable<TEntity> GetAllAsync();
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity id);
    }
}

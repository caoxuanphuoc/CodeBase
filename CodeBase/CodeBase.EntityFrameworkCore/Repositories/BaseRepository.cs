using CodeBase.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.EntityFrameworkCore.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : EntityAuditBase<TKey>
    {
        private readonly CodebBaseDbContext _context;

        public BaseRepository(CodebBaseDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            _context.Set<TEntity>().Update(entity);
        }

        public IQueryable<TEntity> GetAllAsync()
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking().Where(entity => !entity.IsDeleted);
            return query;
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking().Where(entity => !entity.IsDeleted);
            return await query.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }


        public void UpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _context.Set<TEntity>().Update(entity);
        }
    }
}

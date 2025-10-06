using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Infrastructure.Repositories
{
    public class ReadOnlyRepository<T, TId> : IReadOnlyRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        public ReadOnlyRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
            dbSet = dbContext.Set<T>();
        }
        public async Task<T> GetByIdAsync(TId id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity!;
        }
        public async Task<T> GetByIdAsync(TId id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = await query.FirstOrDefaultAsync(e => e.Id!.Equals(id));
            return entity!;
        }

        public async Task<IEnumerable<T>> GetPaged(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy = null!, bool sortAscending = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = sortAscending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            return await query.Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();
        }
    }
}

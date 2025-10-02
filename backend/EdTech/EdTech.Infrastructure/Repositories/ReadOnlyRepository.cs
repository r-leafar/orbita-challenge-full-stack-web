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
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<T> dbSet;
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

        public async Task<IEnumerable<T>> GetPaged(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy = null!, bool sortAscending = true)
        {
            IQueryable<T> query = dbContext.Set<T>();

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

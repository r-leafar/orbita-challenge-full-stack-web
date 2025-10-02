using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Infrastructure.Repositories
{
    public class WriteOnlyRepository<T, TId> : IWriteOnlyRepository<T, TId> where T : BaseEntity<TId>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<T> dbSet;
        public WriteOnlyRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
            dbSet = dbContext.Set<T>();
        }

        public async Task<TId> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}

using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Infrastructure.Repositories
{
    public class ReadAndWriteRepository<T, TId> : IReadOnlyRepository<T, TId>, IWriteOnlyRepository<T, TId> where T : BaseEntity<TId>
    {
        private readonly ReadOnlyRepository<T, TId> repositoryRead;
        private readonly WriteOnlyRepository<T, TId> repositoryWrite;

        public ReadAndWriteRepository(ApplicationDbContext _dbContext)
        {
            repositoryRead = new ReadOnlyRepository<T, TId>(_dbContext ?? throw new ArgumentNullException(nameof(_dbContext)));
            repositoryWrite = new WriteOnlyRepository<T, TId>(_dbContext);
        }
        public async Task<TId> AddAsync(T entity)
        {
            return await repositoryWrite.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await repositoryWrite.DeleteAsync(entity);
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await repositoryRead.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetPaged(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy = null, bool sortAscending = true)
        {
            return await repositoryRead.GetPaged(pageNumber, pageSize, orderBy, sortAscending);
        }

        public async Task UpdateAsync(T entity)
        {
            await repositoryWrite.UpdateAsync(entity);
        }
    }
}

using EdTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Interfaces.Repositories
{
    public interface IWriteOnlyRepository<T, TId> where T : BaseEntity<TId>
    {

        public Task<TId> AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}

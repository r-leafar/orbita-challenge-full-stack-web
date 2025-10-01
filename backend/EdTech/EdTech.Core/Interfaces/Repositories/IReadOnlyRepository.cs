using EdTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Interfaces.Repositories
{
    public interface IReadOnlyRepository<T, TId> where T : BaseEntity<TId>
    {
        public Task<T> GetByIdAsync(TId id);

        public Task<IEnumerable<T>> GetPaged(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy = null!, bool sortAscending = true);
    }
}

using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Exceptions
{
    public static class RepositoryExtensions
    {
        public static async Task<T> GetByIdOrThrowAsync<T, TId>(
            this IReadOnlyRepository<T, TId> repository,
            TId id,
            string? entityName = null) where T : BaseEntity<TId> 
        {
            var entity = await repository.GetByIdAsync(id);

            if (entity == null)
            {
                var name = entityName ?? typeof(T).Name;
                throw new ApplicationException($"{name} com Id {id} não encontrado.");
            }

            return entity;
        }
      public static async Task<T> GetByIdOrThrowAsync<T, TId>(
      this IReadOnlyRepository<T, TId> repository,
      TId id,
      string? entityName = null,
      params Expression<Func<T, object>>[] includes)
      where T : BaseEntity<TId>
        {
            var entity = await repository.GetByIdAsync(id, includes);

            if (entity == null)
            {
                var name = entityName ?? typeof(T).Name;
                throw new ApplicationException($"{name} com Id {id} não encontrado.");
            }

            return entity;
        }
    }
}

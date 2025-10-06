using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;
using EdTech.Infrastructure.Exceptions;
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
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> dbSet;
        public WriteOnlyRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
            dbSet = dbContext.Set<T>();
        }

        public async Task<TId> AddAsync(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Ocorreu um erro ao cadastrar", ex);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Ocorreu um erro ao deletar o registro", ex);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
            dbSet.Update(entity);
            await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InfraestructureException("Ocorreu um erro ao atualizar o registro", ex);
            }
        }
    }
}

using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EdTech.Infrastructure.Repositories
{
    public class StudentRepository : ReadAndWriteRepository<Student, Guid>, IStudentRepository
    {
        private readonly ApplicationDbContext dbContext;
        public StudentRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
            this.dbContext = _dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
        }

        public async Task<IEnumerable<Student>> GetByNamePaged(int pageNumber, int pageSize, string name, params Expression<Func<Student, object>>[] includes)
        {
            IQueryable<Student> query = dbContext.Students;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                // Usa Contains para encontrar o nome em qualquer parte do campo (LIKE '%nome%')
                query = query.Where(s => s.Name.ToLower().Contains(name.ToLower()));
            }
            int skip = (pageNumber - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);


            return await query.ToListAsync();
        }
    }
}

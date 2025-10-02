using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Infrastructure.Context;

namespace EdTech.Infrastructure.Repositories
{
    public class StudentRepository : ReadAndWriteRepository<Student, Guid>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}

using EdTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Interfaces.Repositories
{
    public interface IStudentRepository : IWriteOnlyRepository<Student, Guid>, IReadOnlyRepository<Student, Guid>
    {

        public  Task<IEnumerable<Student>> GetByNamePaged(int pageNumber, int pageSize, string name, params Expression<Func<Student, object>>[] includes);
    }
}

using EdTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Interfaces.Repositories
{
    public interface IStudentRepository : IWriteOnlyRepository<Student, Guid>, IReadOnlyRepository<Student, Guid>
    {
    }
}

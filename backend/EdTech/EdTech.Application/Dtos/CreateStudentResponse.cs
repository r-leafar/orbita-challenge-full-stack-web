using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Dtos
{
    public record CreateStudentResponse(
        Guid Id,
        string Name,
        string Email,
        string SchoolId,
        string NationalIdValue
    );
}

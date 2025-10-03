using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Dtos.Requests
{
    public record CreateStudentRequest(
        string Name,
        string Email,
        string SchoolId,
        string NationalIdType,
        string NationalIdValue
    );
}

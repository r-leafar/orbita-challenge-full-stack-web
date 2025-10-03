using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Dtos.Requests
{
    public record UpdateStudentRequest(
        Guid Id,
        string Name,
        string Email
    );
}

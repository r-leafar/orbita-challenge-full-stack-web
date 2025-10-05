using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Dtos.Responses
{
    public record StudentResponse(
        Guid Id,
        string Name,
        string Email,
        string studentId,
        string NationalIdValue
    );
}

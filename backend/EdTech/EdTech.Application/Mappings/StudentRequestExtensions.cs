using EdTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Mappings
{
    public static class StudentRequestExtensions
    {
        public static CreateStudentResponse ToResponse(this CreateStudentRequest request, Guid id)
        {
            return new CreateStudentResponse(
                id,
                request.Name,
                request.Email,
                request.SchoolId,
                request.NationalIdValue
            );
        }
    }
}

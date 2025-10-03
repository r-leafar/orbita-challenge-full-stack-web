using EdTech.Application.Dtos.Requests;
using EdTech.Application.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Mappings
{
    public static class StudentRequestExtensions
    {
        public static StudentResponse ToResponse(this CreateStudentRequest request, Guid id)
        {
            return new StudentResponse(
                id,
                request.Name,
                request.Email,
                request.SchoolId,
                request.NationalIdValue
            );
        }
    }
}

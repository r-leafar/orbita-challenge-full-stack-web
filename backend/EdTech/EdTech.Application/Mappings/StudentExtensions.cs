using EdTech.Application.Dtos.Requests;
using EdTech.Application.Dtos.Responses;
using EdTech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Mappings
{
    public static class StudentExtensions
    {
        public static StudentResponse ToResponse(this Student student)
        {
            return new StudentResponse(
                student.Id,
                student.Name,
                student.Email,
                student.SchoolId,
                student.NationalIdentifier.Number
            );
        }
    }
}

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
                student.studentId,
                student.NationalIdentifier.Number
            );
        }
        public static IEnumerable<StudentResponse> ToResponse(this IEnumerable<Student> students)
        {
            if (students == null)
            {
                return null;
            }

            return students.Select(student => new StudentResponse(
                student.Id,
                student.Name,
                student.Email,
                student.studentId,
                student.NationalIdentifier?.Number));
        }
    }
}

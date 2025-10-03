using EdTech.Application.Dtos;
using EdTech.Application.Dtos.Responses;
using EdTech.Application.Exceptions;
using EdTech.Application.Mappings;
using EdTech.Core.Entities;
using EdTech.Core.Exceptions;
using EdTech.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.UseCases.Query
{
    public class GetStudentByIdQuery : IQueryById<StudentResponse, Guid>
    {
        private readonly IStudentRepository _repository;

        public GetStudentByIdQuery(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<StudentResponse> Handle(Guid id)
        {
            try
            {
               var student = await _repository.GetByIdOrThrowAsync(id, "Student", x => x.NationalIdentifier);
               return student.ToResponse();
            }
            catch (DomainException ex)
            {
                throw new Exceptions.ApplicationException("Erro ao consultar o estudante.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}

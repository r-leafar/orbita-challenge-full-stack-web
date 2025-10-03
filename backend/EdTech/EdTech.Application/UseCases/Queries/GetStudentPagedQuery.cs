using EdTech.Application.Dtos;
using EdTech.Application.Dtos.Responses;
using EdTech.Application.Exceptions;
using EdTech.Application.Mappings;
using EdTech.Application.Validation;
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
    public class GetStudentPagedQuery
    {
        private readonly IStudentRepository _repository;

        public GetStudentPagedQuery(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<StudentResponse>> Handle(int page,int pageSize)
        {
            PaginationValidator.Validate(page, pageSize);

            try
            {
              var students = await _repository.GetPaged(page,pageSize, x=> x.Name,true, x => x.NationalIdentifier);

               return new PagedResponse<StudentResponse>(students.ToResponse(),page, pageSize,0,students.Count());
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

using EdTech.Application.Dtos.Responses;
using EdTech.Core.Interfaces.Repositories;
using EdTech.Application.Mappings;

namespace EdTech.Application.UseCases.Queries
{
    public class GetStudentByNamePaged
    {
        private readonly IStudentRepository _repository;

        public GetStudentByNamePaged(IStudentRepository repository)
        {
            _repository = repository;
        }
        public async Task<PagedResponse<StudentResponse>> Handle(string name, int page, int pageSize)
        {
            try
            {
                var students = await _repository.GetByNamePaged(page,pageSize, name, x => x.NationalIdentifier);

                return new PagedResponse<StudentResponse>(students.ToResponse(), page, pageSize, 0, students.Count());
            }
            catch (Exceptions.ApplicationException ex)
            {
                throw new Exceptions.ApplicationException("Erro ao consultar o nome do estudante.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

    }
}

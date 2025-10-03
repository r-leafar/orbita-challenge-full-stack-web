using EdTech.Application.Exceptions;
using EdTech.Core.Exceptions;
using EdTech.Core.Interfaces.Repositories;

namespace EdTech.Application.UseCases
{
    public class DeleteStudentCommand
    {
        private readonly IStudentRepository _repository;


        public DeleteStudentCommand(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(Guid id)
        {

            var student = await _repository.GetByIdOrThrowAsync(id);

            try
            {
                await _repository.DeleteAsync(student);

            }catch(DomainException ex)
            {
                throw new EdTech.Application.Exceptions.ApplicationException("Erro ao apagar o estudante.", ex);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}

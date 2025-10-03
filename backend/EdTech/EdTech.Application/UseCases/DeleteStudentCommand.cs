using EdTech.Application.Dtos;
using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Factories;
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

            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                throw new EdTech.Application.Exceptions.ApplicationException($"Estudante com Id {id} não encontrado.");
            }
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

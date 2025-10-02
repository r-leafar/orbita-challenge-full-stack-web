using EdTech.Application.Dtos;
using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Factories;
using EdTech.Core.Interfaces.Repositories;

namespace EdTech.Application.UseCases
{
    public class UpdateStudentCommand
    {
        private readonly IStudentRepository _repository;


        public UpdateStudentCommand(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateStudentRequest dto)
        {

            var student = await _repository.GetByIdAsync(dto.Id);

            if (student == null)
            {
                throw new EdTech.Application.Exceptions.ApplicationException($"Estudante com Id {dto.Id} não encontrado.");
            }
            try
            {
                student.Email = dto.Email;
                student.Name = dto.Name;

                await _repository.UpdateAsync(student);

            }catch(DomainException ex)
            {
                throw new EdTech.Application.Exceptions.ApplicationException("Erro ao atualizar estudante.", ex);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}

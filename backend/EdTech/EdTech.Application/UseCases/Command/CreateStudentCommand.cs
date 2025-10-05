using EdTech.Application.Dtos.Requests;
using EdTech.Application.Exceptions;
using EdTech.Application.Validation;
using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Factories;
using EdTech.Core.Interfaces.Repositories;

namespace EdTech.Application.UseCases.Command
{
    public class CreateStudentCommand
    {
        private readonly IStudentRepository _repository;


        public CreateStudentCommand(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateStudentRequest dto)
        {
            var enumNationalIdentifier = NationalIdentifierValidation.Validate(dto.NationalIdType);

            var student = new Student(
                   name: dto.Name,
                   email: dto.Email,
                   studentId: dto.studentId,
                   nationalIdentifier : NationalIdentifierFactory.Create(enumNationalIdentifier, dto.NationalIdValue)
               );

            await _repository.AddAsync(student);

            return student.Id;
        }

    }
}

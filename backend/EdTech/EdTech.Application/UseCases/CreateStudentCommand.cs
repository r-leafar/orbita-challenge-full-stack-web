using EdTech.Application.Dtos;
using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Factories;
using EdTech.Core.Interfaces.Repositories;

namespace EdTech.Application.UseCases
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
            NationalIdentifierType enumNationalIdentifier = NationalIdentifierType.NONE;

           Enum.TryParse<NationalIdentifierType>(dto.NationalIdType,ignoreCase: true, out enumNationalIdentifier);

            if (enumNationalIdentifier == NationalIdentifierType.NONE)
            {
                throw new ArgumentException("O tipo do indentificador informado é inválido.", nameof(dto.NationalIdType));
            }

            var student = new Student(
                   name: dto.Name,
                   email: dto.Email,
                   schoolId: dto.SchoolId,
                   nationalIdentifier : NationalIdentifierFactory.Create(enumNationalIdentifier, dto.NationalIdValue)
               );

            await _repository.AddAsync(student);

            return student.Id;
        }

    }
}

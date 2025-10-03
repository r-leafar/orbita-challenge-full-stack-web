using EdTech.Application.UseCases.Query;
using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.UnitTest.Helpers;
using Moq;

namespace EdTech.UnitTest.Application.Command
{
    public class GetStudentByIdQueryTest
    {
        private readonly Mock<IStudentRepository> _repositoryMock = new Mock<IStudentRepository>();
        private readonly GetStudentByIdQuery _query;

        public GetStudentByIdQueryTest()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _query = new GetStudentByIdQuery(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetStudentByIdQuery_WithSucess()
        {
            var id = Guid.CreateVersion7();
            var name = "Rafael";
            var email = "shja@hh.com";
            var schoolId = "123";

            var student = new Student(name, email, schoolId, NationalIdentifierTestHelper.CreateValidCpf());

            _repositoryMock.Setup(r => r.GetByIdAsync(id, x=> x.NationalIdentifier)).ReturnsAsync(student);
            
            var result = await _query.Handle(id);

            Assert.Equal("Rafael", result.Name);
            Assert.Equal(email, result.Email);
            Assert.Equal(schoolId, result.SchoolId);
        }

        [Fact]
        public async Task GetStudentByIdQuery_StudentNotFound_ShouldThrowException()
        {
            var id = Guid.Parse("0199abd8-229a-71e7-9d7c-5ddda3f116dd");

            _repositoryMock.Setup(r => r.GetByIdAsync(id, x => x.NationalIdentifier)).ReturnsAsync((Student)null!);

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException> ( async() =>
            {
                await _query.Handle(id);
            });

            Assert.Equal("Erro ao consultar o estudante.", exception.Message);
            Assert.Equal("Student com Id 0199abd8-229a-71e7-9d7c-5ddda3f116dd não encontrado.", exception?.InnerException?.Message);
        }
    }
}

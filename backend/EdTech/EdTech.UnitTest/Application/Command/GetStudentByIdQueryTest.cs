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
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(id, x=> x.NationalIdentifier)).ReturnsAsync(student);
            
            var result = await _query.Handle(id);

            Assert.Equal(student.Name, result.Name);
            Assert.Equal(student.Email, result.Email);
            Assert.Equal(student.SchoolId, result.SchoolId);
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
        [Fact]
        public async Task GetStudentByIdQuery_WithError_ShouldThrowException()
        {
            var id = Guid.Parse("0199abd8-229a-71e7-9d7c-5ddda3f116dd");
            _repositoryMock.Setup(r => r.GetByIdAsync(id, x => x.NationalIdentifier)).ThrowsAsync(new Exception("Database error"));
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _query.Handle(id);
            });

            Assert.Equal("Exception of type 'System.Exception' was thrown.",exception.Message);
        }
    }
}

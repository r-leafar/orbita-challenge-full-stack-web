using EdTech.Application.Dtos.Requests;
using EdTech.Application.UseCases.Command;
using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using Moq;

namespace EdTech.UnitTest.Application.Command
{
    public class CreateStudentCommandTest
    {
        private readonly Mock<IStudentRepository> _repositoryMock = new Mock<IStudentRepository>();
        private readonly CreateStudentCommand _command;

        public CreateStudentCommandTest()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _command = new CreateStudentCommand(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateStudentCommand_WithSucess()
        {
            var id = Guid.Parse("0199abd8-229a-71e7-9d7c-5ddda3f116dd");
            var dto = CreateStudentRequestStub();

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Student>())).ReturnsAsync(id);

            _= await _command.Handle(dto);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Once);
        }
        [Fact]
        public async Task CreateStudentCommand_ForNationalIdTypeInvalid_ShouldThrowApplicationException()
        {
            var id = Guid.Parse("0199abd8-229a-71e7-9d7c-5ddda3f116dd");
            var dto = CreateStudentRequestStub();
            dto = dto with { NationalIdType = "INVALID" };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Student>())).ReturnsAsync(id);

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException>(async () =>
            {
                _ = await _command.Handle(dto);
            });

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Never);

            Assert.Equal("O tipo de identificador 'INVALID' é inválido. (Parâmetro: nationalIdType)", exception.Message);
        }

        [Fact]
        public async Task CreateStudentCommand_ForNationalIdTypeEmpty_ShouldThrowApplicationException()
        {
            var id = Guid.Parse("0199abd8-229a-71e7-9d7c-5ddda3f116dd");
            var dto = CreateStudentRequestStub();
            dto = dto with { NationalIdType = "" };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Student>())).ReturnsAsync(id);

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException>(async () =>
            {
                _ = await _command.Handle(dto);
            });

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Student>()), Times.Never);

            Assert.Equal("O tipo do identificador não pode ser vazio. (Parâmetro: nationalIdType)", exception.Message);
        }

        private CreateStudentRequest CreateStudentRequestStub()
        {
            return new CreateStudentRequest("Diego","jsl@ll.com","1234","CPF", "12345678911");
        }
    }
}

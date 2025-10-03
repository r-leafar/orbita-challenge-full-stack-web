using EdTech.Application.Dtos.Requests;
using EdTech.Application.UseCases.Command;
using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.UnitTest.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.UnitTest.Application.Command
{
    public class UpdateStudentCommandTest
    {
        private readonly Mock<IStudentRepository> _repositoryMock = new Mock<IStudentRepository>();
        private readonly UpdateStudentCommand _command;

        public UpdateStudentCommandTest()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _command = new UpdateStudentCommand(_repositoryMock.Object);
        }

        [Fact]
        public async Task UpdateStudent_WithSucess()
        {
            var dto = CreateUpdateStudentRequestStub();
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);

            await _command.Handle(dto);

            _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Student>()), Times.Once);
        }
        [Fact]
        public async Task UpdateStudent_WithError_ShouldThrowApplicationException()
        {
            var dto = CreateUpdateStudentRequestStub();
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Student>())).ThrowsAsync(new EdTech.Application.Exceptions.ApplicationException(""));

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException> (async () =>
            {
                await _command.Handle(dto);
            });

            _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Student>()), Times.Once);
            
            Assert.Equal("Erro ao atualizar estudante.", exception.Message);
        }

        [Fact]
        public async Task UpdateStudent_WithError_ShouldThrowException()
        {
            var dto = CreateUpdateStudentRequestStub();
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Student>())).ThrowsAsync(new Exception());

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _command.Handle(dto);
            });

            _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Student>()), Times.Once);

            Assert.Equal("Exception of type 'System.Exception' was thrown.", exception.Message);
        }
        private UpdateStudentRequest CreateUpdateStudentRequestStub()
        {
            return new UpdateStudentRequest(It.IsAny<Guid>(),"Diego", "jsl@ll.com");
        }
    }
}

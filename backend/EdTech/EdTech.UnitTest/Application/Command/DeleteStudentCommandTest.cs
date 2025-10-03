using EdTech.Application.Exceptions;
using EdTech.Application.UseCases.Command;
using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.UnitTest.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.UnitTest.Application.Command
{
    public class DeleteStudentCommandTest
    {
        private readonly Mock<IStudentRepository> _repositoryMock = new Mock<IStudentRepository>();
        private readonly DeleteStudentCommand _command;

        public DeleteStudentCommandTest()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _command = new DeleteStudentCommand(_repositoryMock.Object);
        }

        [Fact]
        public async Task DeleteStudentCommand_WithSucess()
        {
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);

            await _command.Handle(It.IsAny<Guid>());

            _repositoryMock.Verify(r =>r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Student>()), Times.Once);
        }
        [Fact]
        public async Task DeleteStudentCommand_StudentNotFound_ShouldThrowApplicationException()
        {
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => null!);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException>(async () =>
            {
                await _command.Handle(It.IsAny<Guid>());
            });

            _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Student>()), Times.Never);
            Assert.Equal("Student com Id 00000000-0000-0000-0000-000000000000 não encontrado.", exception.Message);
        }

        [Fact]
        public async Task DeleteStudentCommand_ErroOnDelete_ShouldThrowException()
        {
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Student>())).ThrowsAsync(new Exception());

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _command.Handle(It.IsAny<Guid>());
            });

            _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Student>()), Times.Once);
            Assert.Equal("Exception of type 'System.Exception' was thrown.", exception.Message);
        }

        [Fact]
        public async Task DeleteStudentCommand_ErroOnDelete_ShouldThrowApplicationException()
        {
            var student = StudentTestHelper.CreateValidStudent_V1();

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(student);
            _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Student>())).ThrowsAsync(new EdTech.Application.Exceptions.ApplicationException(""));

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException>(async () =>
            {
                await _command.Handle(It.IsAny<Guid>());
            });

            _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Student>()), Times.Once);
            Assert.Equal("Erro ao apagar o estudante.", exception.Message);
        }
    }
}

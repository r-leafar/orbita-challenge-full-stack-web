using EdTech.Application.Exceptions;
using EdTech.Application.UseCases.Query;
using EdTech.Core.Entities;
using EdTech.Core.Interfaces.Repositories;
using EdTech.UnitTest.Helpers;
using Moq;
using System.Linq.Expressions;

namespace EdTech.UnitTest.Application.Command
{
    public class GetStudentPagedQueryTest
    {
        private readonly Mock<IStudentRepository> _repositoryMock = new Mock<IStudentRepository>();
        private readonly GetStudentPagedQuery _query;

        public GetStudentPagedQueryTest()
        {
            _repositoryMock = new Mock<IStudentRepository>();
            _query = new GetStudentPagedQuery(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetStudentPagedQuery_WithSucess()
        {
            int page = 1;
            int pageSize = 10;
            var studentList = StudentTestHelper.CreateListOfValidStudent();

            _repositoryMock.Setup(r => r.GetPaged(page, pageSize, x => x.Name, true, x => x.NationalIdentifier)).ReturnsAsync(studentList);

            var result = await _query.Handle(1, 10);

            _repositoryMock.Verify(r => r.GetPaged(
                page,
                pageSize,
                It.IsAny<Expression<Func<Student, object>>>(), 
                It.IsAny<bool>(),                              
                It.IsAny<Expression<Func<Student, object>>[]>()
            ),
            Times.Once);

            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(studentList.Count, result.TotalRecords);
            Assert.Equal(page, result.PageNumber);
        }

        [Fact]
        public async Task GetStudentPagedQuery_WithError_ShouldThrowApplicationException()
        {
            int page = 1;
            int pageSize = 10;

            _repositoryMock.Setup(r => r.GetPaged(page, pageSize, x => x.Name, true, x => x.NationalIdentifier)).ThrowsAsync(new EdTech.Application.Exceptions.ApplicationException(""));

            var exception = await Assert.ThrowsAsync<EdTech.Application.Exceptions.ApplicationException>(async () =>
            {
                var result = await _query.Handle(1, 10);

            });

            _repositoryMock.Verify(r => r.GetPaged(
                page,
                pageSize,
                It.IsAny<Expression<Func<Student, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<Expression<Func<Student, object>>[]>()
            ),
            Times.Once);

            Assert.Equal("Erro ao consultar o estudante.", exception.Message);
        }
        [Fact]
        public async Task GetStudentPagedQuery_WithError_ShouldThrowException()
        {
            int page = 1;
            int pageSize = 10;

            _repositoryMock.Setup(r => r.GetPaged(page, pageSize, x => x.Name, true, x => x.NationalIdentifier)).ThrowsAsync(new Exception());

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                var result = await _query.Handle(1, 10);

            });

            _repositoryMock.Verify(r => r.GetPaged(
                page,
                pageSize,
                It.IsAny<Expression<Func<Student, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<Expression<Func<Student, object>>[]>()
            ),
            Times.Once);

            Assert.Equal("Exception of type 'System.Exception' was thrown.", exception.Message);
        }
    }
}

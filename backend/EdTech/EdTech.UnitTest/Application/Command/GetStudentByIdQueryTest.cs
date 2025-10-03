using EdTech.Application.UseCases.Command;
using EdTech.Application.UseCases.Query;
using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Factories;
using EdTech.Core.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var student = new Student(name, email, schoolId, createCpf());

            _repositoryMock.Setup(r => r.GetByIdAsync(id, x=> x.NationalIdentifier)).ReturnsAsync(student);
            
            var result = await _query.Handle(id);

            Assert.Equal("Rafael", result.Name);
            Assert.Equal(email, result.Email);
            Assert.Equal(schoolId, result.SchoolId);
        }
        private NationalIdentifier createCpf()
        {
            return NationalIdentifierFactory.Create(NationalIdentifierType.CPF, "12345678900");
        }
    }
}

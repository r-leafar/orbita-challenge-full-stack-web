using EdTech.Core.Entities;
using EdTech.Core.Exceptions;
using EdTech.UnitTest.Helpers;

namespace EdTech.UnitTest.Core
{
    public class StudentTest
    {
        [Fact]
        public void CreateStudent_WithSucess_ShouldBeNotNull()
        {
            var student = StudentTestHelper.CreateValidStudent();

            Assert.NotNull(student);
        }

        [Fact]
        public void CreateStudent_WithEmptyName_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = StudentTestHelper.CreateInvalidStudent_NoName();
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: name)", exception.Message);
        }
        [Fact]
        public void CreateStudent_WithEmptyEmail_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = StudentTestHelper.CreateInvalidStudent_NoEmail();
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: email)", exception.Message);
        }

        [Fact]
        public void CreateStudent_WithEmptySchoolId_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = StudentTestHelper.CreateInvalidStudent_NoSchoolId();
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: schoolId)", exception.Message);
        }

        [Fact]
        public void CreateStudent_WithEmptyNationalIdentifier_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = new Student("Rafael", "shja@lg.com", "1234", null);
            });

            Assert.Equal("nationalIdentifier (Parâmetro: O valor não pode ser nulo.)", exception.Message);
        }

        [Fact]
        public void ChangeStudentName_ForEmpty_ShouldThrowException()
        {
            var student = StudentTestHelper.CreateValidStudent();

            var exception = Assert.Throws<DomainException>(() =>
            {
                student.Name = "";
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: Name)", exception.Message);
        }

        [Fact]
        public void ChangeStudentEmail_ForEmpty_ShouldThrowException()
        {
            var student = StudentTestHelper.CreateValidStudent();

            var exception = Assert.Throws<DomainException>(() =>
            {
                student.Email = "";
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: Email)", exception.Message);
        }
    }
}

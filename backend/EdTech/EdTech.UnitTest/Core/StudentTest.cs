using EdTech.Core.Entities;
using EdTech.Core.Exceptions;

namespace EdTech.UnitTest.Core
{
    public class StudentTest
    {
        [Fact]
        public void CreateStudent_WithSucess_ShouldBeNotNull()
        {
            var student = createStudent();

            Assert.NotNull(student);
        }

        [Fact]
        public void CreateStudent_WithEmptyName_ShouldThrowException()
        {
            var nationalId = new CpfIdentifier("12345678900");

            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = new Student("", "Saboia@lg.com", "123", nationalId);
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: name)", exception.Message);
        }
        [Fact]
        public void CreateStudent_WithEmptyEmail_ShouldThrowException()
        {
            var nationalId = new CpfIdentifier("12345678900");

            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = new Student("Rafael", "", "123", nationalId);
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: email)", exception.Message);
        }

        [Fact]
        public void CreateStudent_WithEmptySchoolId_ShouldThrowException()
        {
            var nationalId = new CpfIdentifier("12345678900");

            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = new Student("Rafael", "shja@lg.com", "", nationalId);
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: schoolId)", exception.Message);
        }

        [Fact]
        public void CreateStudent_WithEmptyNationalIdentifier_ShouldThrowException()
        {
            var nationalId = new CpfIdentifier("12345678900");

            var exception = Assert.Throws<DomainException>(() =>
            {
                var student = new Student("Rafael", "shja@lg.com", "1234", null);
            });

            Assert.Equal("nationalIdentifier (Parâmetro: O valor não pode ser nulo.)", exception.Message);
        }

        [Fact]
        public void ChangeStudentName_ForEmpty_ShouldThrowException()
        {
            var student = createStudent();

            var exception = Assert.Throws<DomainException>(() =>
            {
                student.Name = "";
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: Name)", exception.Message);
        }

        [Fact]
        public void ChangeStudentEmail_ForEmpty_ShouldThrowException()
        {
            var student = createStudent();

            var exception = Assert.Throws<DomainException>(() =>
            {
                student.Email = "";
            });

            Assert.Equal("O valor não pode ser vazio. (Parâmetro: Email)", exception.Message);
        }

        private Student createStudent() 
        {
            var nationalId = new CpfIdentifier("12345678900");
            var student = new Student("Rafael", "Saboia@gmail.com", "123", nationalId);
            return student;
        }

    }
}

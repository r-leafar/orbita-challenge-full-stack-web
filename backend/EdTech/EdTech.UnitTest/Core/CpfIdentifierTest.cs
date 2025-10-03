using EdTech.Core.Exceptions;
using EdTech.UnitTest.Helpers;

namespace EdTech.UnitTest.Core
{
    public class CpfIdentifierTest
    {

        [Fact]
        public void CreateCPF_WithSucess_ShouldBeNotNull()
        {
            var cpf = NationalIdentifierTestHelper.CreateValidCpf();

            Assert.NotNull(cpf);
        }

        [Fact]
        public void CreateCPF_WithLessThan11Digits_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                var cpf = NationalIdentifierTestHelper.CreateInvalidCpfLessThan11Digits();
            });

            Assert.Equal("O valor informado para o CPF não é válido (Parâmetro: 1234)", exception.Message);
        }

        [Fact]
        public void CreateCPF_WithMoreThan11Digits_ShouldThrowException()
        {
            var exception = Assert.Throws<DomainException>(() =>
            {
                var cpf = NationalIdentifierTestHelper.CreateInvalidCpfMoreThan11Digits();
            });

            Assert.Equal("O valor informado para o CPF não é válido (Parâmetro: 123456789111)", exception.Message);
        }
    }
}

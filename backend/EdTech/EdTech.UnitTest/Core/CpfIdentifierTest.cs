using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.UnitTest.Core
{
    public class CpfIdentifierTest
    {

        [Fact]
        public void CreateCPF_WithSucess_ShouldBeNotNull()
        {
            var cpf = createCpfIdentifier();

            Assert.NotNull(cpf);
        }

        [Fact]
        public void CreateCPF_WithLessThan11Digits_ShouldThrowException()
        {
            var valor = "12345";
            var exception = Assert.Throws<DomainException>(() =>
            {
                var cpf = NationalIdentifierFactory.Create(NationalIdentifierType.CPF, valor);
            });

            Assert.Equal($"O valor informado para o CPF não é válido (Parâmetro: {valor})", exception.Message);
        }

        [Fact]
        public void CreateCPF_WithMoreThan11Digits_ShouldThrowException()
        {
            var valor = "123456789111";
            var exception = Assert.Throws<DomainException>(() =>
            {
                var cpf = NationalIdentifierFactory.Create(NationalIdentifierType.CPF, valor);
            });

            Assert.Equal($"O valor informado para o CPF não é válido (Parâmetro: {valor})", exception.Message);
        }

        private NationalIdentifier createCpfIdentifier()
        {
            return  NationalIdentifierFactory.Create(NationalIdentifierType.CPF, "12345678900");
        }
    }
}

using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Factories;

namespace EdTech.UnitTest.Core
{
    public class NationalIdentifierFactoryTest
    {

        [Fact]
        public void CreateIdentifier_WhenTypeIsCPF_ShouldReturnCpfIdentifierInstance()
        {
            var type = NationalIdentifierType.CPF;
            var value = "12345678911";

            var identifier = NationalIdentifierFactory.Create(type, value);

            Assert.Equal(type, identifier.Type);
            Assert.Equal(value, identifier.Number);
        }

        [Fact]
        public void Create_WhenTypeIsNotSupported_ShouldThrowDomainException()
        {
            var unsupportedType = NationalIdentifierType.NONE;
            var value = "999";

            var exception = Assert.Throws<DomainException>(() =>
            {
                var identifier =  NationalIdentifierFactory.Create(unsupportedType, value);
            });

            Assert.Equal($"O identificador '{unsupportedType}' não é suportado.", exception.Message);
        }
    }
}

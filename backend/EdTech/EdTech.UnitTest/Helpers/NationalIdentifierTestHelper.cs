using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Factories;

namespace EdTech.UnitTest.Helpers
{
    public static class NationalIdentifierTestHelper
    {
        public static NationalIdentifier CreateValidCpf()
        {
            return NationalIdentifierFactory.Create(NationalIdentifierType.CPF, "12345678900");
        }
        public static NationalIdentifier CreateInvalidCpfLessThan11Digits()
        {
            return NationalIdentifierFactory.Create(NationalIdentifierType.CPF, "1234");
        }
        public static NationalIdentifier CreateInvalidCpfMoreThan11Digits()
        {
            return NationalIdentifierFactory.Create(NationalIdentifierType.CPF, "123456789111");
        }
    }
}

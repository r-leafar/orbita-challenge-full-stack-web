using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Exceptions;

namespace EdTech.Core.Factories
{
    public static class NationalIdentifierFactory
    {
        public static NationalIdentifier Create(NationalIdentifierType type, string value)
        {
            return type switch
            {
                NationalIdentifierType.CPF => new CpfIdentifier(value),
                _ => throw new DomainException($"O identificador '{type}' não é suportado.")
            };
        }
    }
}

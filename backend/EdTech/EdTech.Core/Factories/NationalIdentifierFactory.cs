using EdTech.Core.Entities;
using EdTech.Core.Enums;
using EdTech.Core.Exceptions;

namespace EdTech.Core.Factories
{
    public static class NationalIdentifierFactory
    {
        public static NationalIdentifier Create(NationalIdentifierType type, string value)
        {
            string defaultMessage = $"O identificador '{type}' não é suportado.";

            return type switch
            {
                NationalIdentifierType.CPF => new CpfIdentifier(value),
                NationalIdentifierType.NONE => throw new DomainException(defaultMessage),
                _ => throw new DomainException(defaultMessage)
            };
        }
    }
}

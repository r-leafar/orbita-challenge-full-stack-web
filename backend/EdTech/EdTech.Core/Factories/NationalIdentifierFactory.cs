using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Interfaces;
using EdTech.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

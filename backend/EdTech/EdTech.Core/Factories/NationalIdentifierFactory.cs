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
        public static INationalIdentifier Create(string type, string value)
        {
            return type.ToLower() switch
            {
                "cpf" => new CpfIdentifier(value),
                _ => throw new NotSupportedException($"O identificador '{type}' não é suportado.")
            };
        }
    }
}

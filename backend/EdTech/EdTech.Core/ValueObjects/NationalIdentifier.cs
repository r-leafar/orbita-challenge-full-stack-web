using EdTech.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.ValueObjects
{
    public abstract class NationalIdentifier
    {
        public string Number { get; init; }
        public NationalIdentifierType Type { get; init; }
        protected NationalIdentifier(string number, NationalIdentifierType type)
        {
            Number = number;
            Type = type;

            if (!IsValid())
            {
                throw new ArgumentException($"O número '{number}' não é um identificador válido do tipo '{type}'.");
            }
        }
        protected abstract bool IsValid();

    }
}

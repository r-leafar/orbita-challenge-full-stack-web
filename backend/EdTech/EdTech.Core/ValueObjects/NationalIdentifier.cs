using EdTech.Core.Enums;
using EdTech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.ValueObjects
{
    public class NationalIdentifier : INationalIdentifier
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

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}

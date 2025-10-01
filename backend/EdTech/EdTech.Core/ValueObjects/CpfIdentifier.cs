using EdTech.Core.Enums;
using EdTech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.ValueObjects
{
    public class CpfIdentifier : INationalIdentifier
    {
        public string Number { get; }
        public NationalIdentifierType Type => NationalIdentifierType.CPF;

        public CpfIdentifier(string number)
        {
            Number = number;
        }

        public bool IsValid()
        {
            return Number?.Length == 11;
        }
    }
}

using EdTech.Core.Enums;
using EdTech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.ValueObjects
{
    public class CpfIdentifier : NationalIdentifier
    {
        public CpfIdentifier(string number) : base(number, NationalIdentifierType.CPF)
        {

        }
       
         public override bool IsValid()
        {
            return Number?.Length == 11;
        }
    }
}

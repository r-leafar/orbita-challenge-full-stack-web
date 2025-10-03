using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Entities
{
    public class CpfIdentifier : NationalIdentifier
    {
        private CpfIdentifier() { }
        internal CpfIdentifier(string value) : base(value) 
        {
            if (!IsValid())
                throw new DomainException("O valor informado para o CPF não é válido", Number);  
        }
         public override bool IsValid()
        {
            return Number?.Length == 11;
        }
    }
}

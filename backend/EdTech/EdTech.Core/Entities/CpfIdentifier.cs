using EdTech.Core.Enums;
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
        public CpfIdentifier(string value) : base(value) { }
         public override bool IsValid()
        {
            return Number?.Length == 11;
        }
    }
}

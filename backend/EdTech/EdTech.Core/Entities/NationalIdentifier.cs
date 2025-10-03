using EdTech.Core.Enums;
using EdTech.Core.Exceptions;
using EdTech.Core.Factories;
using EdTech.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Entities
{
    public abstract class NationalIdentifier 
    {
        public Guid StudentId { get; protected set; }
        public string Number { get; init; }
        public NationalIdentifierType Type { get; init; }
        protected NationalIdentifier() { }

        protected NationalIdentifier(string value) 
        {
            Number = value;
        }
        public abstract bool IsValid();
    }
}

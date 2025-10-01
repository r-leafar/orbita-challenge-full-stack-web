using EdTech.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Interfaces
{
    public interface INationalIdentifier
    {
        string Number { get; }
        NationalIdentifierType Type { get; }
        bool IsValid();
    }
}

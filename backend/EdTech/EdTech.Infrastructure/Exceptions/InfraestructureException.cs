using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Infrastructure.Exceptions
{
    public class InfraestructureException : Exception
    {
        public InfraestructureException(string message) : base(message) { }

        public InfraestructureException(string message, string paramName) : this(message + $" (Parâmetro: {paramName})") { }
        public InfraestructureException(string message, Exception innerException)
      : base(message, innerException) { }
    }
}

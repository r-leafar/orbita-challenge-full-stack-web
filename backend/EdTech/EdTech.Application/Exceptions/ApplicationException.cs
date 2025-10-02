using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }

        public ApplicationException(string message,string paramName) : this(message + $" (Parâmetro: {paramName})") { }
        public ApplicationException(string message, Exception innerException)
      : base(message, innerException) { }
    }
}
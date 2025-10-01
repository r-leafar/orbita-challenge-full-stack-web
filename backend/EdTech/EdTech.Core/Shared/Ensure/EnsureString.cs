using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Shared.Ensure
{
    public static partial class Ensure
    {

        public static void NotNullOrWhiteSpace(string? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            NotNull(value, paramName);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O valor não pode ser vazio.", paramName ?? nameof(value));
            }
        }
        public static void NotNull(string? value, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName ?? nameof(value), "O valor não pode ser nulo.");
            }
        }
    }
}

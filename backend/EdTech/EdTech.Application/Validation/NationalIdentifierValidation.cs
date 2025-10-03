using EdTech.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Validation
{
    public static class NationalIdentifierValidation
    {
        public static NationalIdentifierType Validate(string nationalIdType)
        {
            if (string.IsNullOrWhiteSpace(nationalIdType))
            {
                throw new Exceptions.ApplicationException(
                    "O tipo do identificador não pode ser vazio.",
                    nameof(nationalIdType));
            }

            if (!Enum.TryParse<NationalIdentifierType>(nationalIdType, ignoreCase: true, out var result) ||
                result == NationalIdentifierType.NONE)
            {
                throw new Exceptions.ApplicationException(
                    $"O tipo de identificador '{nationalIdType}' é inválido.",
                    nameof(nationalIdType));
            }

            return result;
        }
    }

}

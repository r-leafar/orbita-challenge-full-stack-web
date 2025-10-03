using EdTech.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Validation
{
    public static class PaginationValidator
    {
        public static void Validate(int page, int pageSize)
        {
            if (page <= 0)
            {
                throw new EdTech.Application.Exceptions.ApplicationException(nameof(page), "O número da página deve ser maior que zero.");
            }

            if (pageSize <= 0)
            {
                throw new EdTech.Application.Exceptions.ApplicationException(nameof(pageSize), "O tamanho da pagina deve ser maior que zero.");
            }
        }
    }
}

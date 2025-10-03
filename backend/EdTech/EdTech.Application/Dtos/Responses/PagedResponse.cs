using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.Dtos.Responses
{
    public record PagedResponse<T>(
        IEnumerable<T> Data,
        int PageNumber,
        int PageSize,
        int TotalPages,
        int TotalRecords
    );
}

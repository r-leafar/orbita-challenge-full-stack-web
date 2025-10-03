using EdTech.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Application.UseCases.Query
{
    public interface IQueryById<TResponse,TId>
    {
        public Task<TResponse> Handle(TId Id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdTech.Core.Entities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; private set; } = default!;

        protected void SetId(TId id)
        {
            Id = id;
        }

    }
}

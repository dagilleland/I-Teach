using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Infrastructure.Specifications
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
    }
}

// Credits: https://en.wikipedia.org/wiki/Specification_pattern#C.23

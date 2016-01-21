using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Infrastructure.Specifications
{
    internal class NotSpecification<TEntity> : ISpecification<TEntity>
    {
        private readonly ISpecification<TEntity> _wrapped;

        protected ISpecification<TEntity> Wrapped
        {
            get
            {
                return _wrapped;
            }
        }

        internal NotSpecification(ISpecification<TEntity> spec)
        {
            if (spec == null)
            {
                throw new ArgumentNullException("spec");
            }

            _wrapped = spec;
        }

        public bool IsSatisfiedBy(TEntity candidate)
        {
            return !Wrapped.IsSatisfiedBy(candidate);
        }
    }
}

// Credits: https://en.wikipedia.org/wiki/Specification_pattern#C.23

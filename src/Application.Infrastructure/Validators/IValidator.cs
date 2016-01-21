using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Validators
{
    public interface IValidator<T>
    {
        bool IsValid(T entity);
        IEnumerable<string> BrokenRules(T entity);
    }
}

// Credits: https://lostechies.com/jimmybogard/2007/10/24/entity-validation-with-visitors-and-extension-methods/


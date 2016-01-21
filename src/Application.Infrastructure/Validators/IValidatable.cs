using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Infrastructure.Validators
{
    public interface IValidatable<T>
    {
        bool Validate(IValidator<T> validator, out IEnumerable<string> brokenRules);
    }
}

// Credits: https://lostechies.com/jimmybogard/2007/10/24/entity-validation-with-visitors-and-extension-methods/

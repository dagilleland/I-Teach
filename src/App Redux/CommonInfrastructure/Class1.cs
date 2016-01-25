using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInfrastructure
{
    public interface IHandleCommand<TCommand>
    {
        IEnumerable<object> Handle(TCommand command);
    }
    public interface IApplyEvent<TEvent>
    {
        void Apply(TEvent theEvent);
    }
}

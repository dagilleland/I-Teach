using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonInfrastructure
{
    public interface IProcessCommand
    {
        void Process<TCommand>(TCommand command) where TCommand : class;
    }
    public interface IPublishEvents
    {
        void PublishEvent(object e);
    }
    public interface IHandleCommand<TCommand> where TCommand:class
    {
        IEnumerable<object> Handle(TCommand command);
    }
    public interface IApplyEvent<TEvent>
    {
        void Apply(TEvent theEvent);
    }
    public interface ISubscribeTo<TEvent>
    {
        void HandleEvent(TEvent occurrence);
    }
}

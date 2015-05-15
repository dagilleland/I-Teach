using System;
using SimpleCqrs;
using SimpleCqrs.EventStore.SqlServer;
using SimpleCqrs.EventStore.SqlServer.Serializers;
using SimpleCqrs.Commanding;
using CourseMapping.Commands;
using StructureMap;
using Microsoft.Practices.Unity;


namespace CourseMapping
{
    public class Application
    {
        ISimpleCqrsRuntime runtime { get; set; }
        private Application(string connectionString)
        {
            runtime = new ApplicationRunTime(connectionString);
        }

        public static Application Instance() { return new Application("DefaultConnection"); }

        public void Process(ICommandWithAggregateRootId command)
        {
            ICommandBus bus = runtime.ServiceLocator.Resolve<ICommandBus>();
            bus.Send(command);
        }

        public class ApplicationRunTime : SimpleCqrsRuntime<UnityServiceLocator>
        {
            private readonly string CONNECTION_STRING;
            public ApplicationRunTime(string connectionString)
            {
                CONNECTION_STRING = connectionString;
            }
            protected override SimpleCqrs.Eventing.IEventStore GetEventStore(IServiceLocator serviceLocator)
            {
                var configuration = new SqlServerConfiguration(CONNECTION_STRING);//,
                //"dbo", "json_event_store");
                return new SqlServerEventStore(configuration, new JsonDomainEventSerializer());
            }
        }

    }
}

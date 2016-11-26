using CommonInfrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.ApplicationHost
{
    public class SchoolApplication : IProcessCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolApplication"/> class.
        /// </summary>
        public SchoolApplication(IPublishEvents eventPublisher)
        {
            Publisher = eventPublisher;
        }
        private readonly static Dictionary<Type, Type> CommandHandlers = new Dictionary<Type, Type>();
        private IPublishEvents Publisher { get; set; }

        ////private readonly string ConnectionStringName;
        //private readonly MessageDispatcher Dispatcher;
        //public IPlanningCalendarRepository PlanningCalendarRepository { get; private set; }

        //private SchoolApplication(string connectionStringName, params object[] subscribers)
        //{
        //    // TODO: Complete member initialization
        //    //this.ConnectionStringName = connectionStringName;
        //    string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        //    Dispatcher = new MessageDispatcher(new SqlEventStore(connectionString));
        //    PlanningCalendarRepository = About.GetPlanningCalendarRepository(connectionStringName);
        //}

        //#region Factory Methods
        //private static SchoolApplication _Instance = null;
        //public static SchoolApplication Instance(params object[] subscribers)
        //{
        //    if (_Instance == null)
        //        _Instance = new SchoolApplication("DefaultConnection", subscribers);

        //    // Register handlers/subscribers for the Course Planning Calendar system
        //    About.GetCommandEventBus(_Instance.Dispatcher).RegisterWithDispatcher(subscribers);

        //    return _Instance;
        //}
        //#endregion

        public void Process<TCommand>(TCommand command) where TCommand:class
        {
            IHandleCommand<TCommand> handler = getHandler<TCommand>();
            var events = handler.Handle(command);
            foreach (var situation in events)
                Publisher.PublishEvent(situation);
        }
        private IHandleCommand<TCommand> getHandler<TCommand>() where TCommand: class
        {
            if (CommandHandlers.ContainsKey(typeof(TCommand)))
            {
                Type commandHandlers = CommandHandlers[typeof(TCommand)];
                return Activator.CreateInstance(commandHandlers) as IHandleCommand<TCommand>;
            }

            var instances = from assm in AppDomain.CurrentDomain.GetAssemblies()
                            from t in assm.GetTypes()
                            where t.GetInterfaces().Contains(typeof(IHandleCommand<TCommand>))
                               && t.GetConstructor(Type.EmptyTypes) != null
                            select t;// Activator.CreateInstance(t) as IHandleCommand<TCommand>;
            if (instances.Count() == 0)
                throw new Exception(string.Format("A command handler for {0} was not found", typeof(TCommand).FullName));
            else if (instances.Count() > 1)
                throw new Exception(string.Format("Too many command handlers for {0}", typeof(TCommand).FullName));

            CommandHandlers.Add(typeof(TCommand), instances.Single());
            return Activator.CreateInstance(instances.Single()) as IHandleCommand<TCommand>;
        }
    }
}

using Autofac;
using Autofac.Core;
using CommonInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp
{
    class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Load the assembly containing the command handlers
            LoadFromAssembly(builder, Assembly.Load("ITeach.School"));
            LoadFromAssembly(builder, Assembly.Load("ITeach.CourseEvaluation"));
            LoadFromAssembly(builder, Assembly.Load("ITeach.ClassList"));
            LoadFromAssembly(builder, Assembly.Load("ITeach.ClassEvaluation"));
            LoadFromAssembly(builder, Assembly.Load("ITeach.StudentMark"));


            // Decorate handlers with loggers
            //builder.RegisterGenericDecorator(typeof(Logger<>),
            //                                 typeof(IHandler<>),
            //                                 "Handler", "Logger");

            // Register the handler resolver
            builder.RegisterType<AutofacHandlerResolver>()
                   .As<IHandlerResolver>();

            // Register the command dispatcher
            builder.RegisterType<CommandDispatcher>()
                   .As<ICommandDispatcher>();
        }

        private void LoadFromAssembly(ContainerBuilder builder, Assembly assembly)
        {
            // Scan the assembly and register keyed services
            builder.RegisterAssemblyTypes(assembly)
                   .As(o => o.GetInterfaces()
                   .Where(i => i.IsClosedTypeOf(typeof(IHandleCommand<>)))
                   .Select(i => new KeyedService("Handler", i)));
        }
    }
    public interface IHandlerResolver
    {
        IHandleCommand<T> Resolve<T>() where T : class;
    }
    public class AutofacHandlerResolver : IHandlerResolver
    {
        private readonly IComponentContext _context;

        public AutofacHandlerResolver(IComponentContext context)
        {
            _context = context;
        }

        public IHandleCommand<T> Resolve<T>() where T : class
        {
            return _context.ResolveOptional<T>();
        }
    }
    public interface ICommandDispatcher
    {
        void Dispatch<T>(T command) where T : class;
    }
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IHandlerResolver _resolver;

        public CommandDispatcher(IHandlerResolver resolver)
        {
            _resolver = resolver;
        }

        public void Dispatch<T>(T command) where T : class
        {
            var handler = _resolver.Resolve<T>();
            if (handler != null)
            {
                handler.Handle(command);
            }
        }
    }
}

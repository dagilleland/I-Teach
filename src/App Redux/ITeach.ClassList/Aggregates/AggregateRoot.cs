using CommonInfrastructure;
using ITeach.ClassList.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.ClassList.Aggregates
{
    public class AggregateRoot
        : IHandleCommand<AddStudent>
        , IHandleCommand<OpenSectionForRegistration>
        , ISubscribeTo<ITeach.School.Events.CourseSectionScheduled>
    {
        private IProcessCommand CommandProcessor { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        public AggregateRoot()
        {
        }
        public AggregateRoot(IProcessCommand commandProcessor)
        {
            this.CommandProcessor = commandProcessor;
        }
        public IEnumerable<object> Handle(AddStudent command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> Handle(OpenSectionForRegistration command)
        {
            throw new NotImplementedException();
        }

        public void HandleEvent(School.Events.CourseSectionScheduled occurrence)
        {
            Console.WriteLine(occurrence);

            // TODO: Get the details for the course offering (call to the upstream domain)
            var offeringId = occurrence.CourseOfferingId;

            // Create a command
            var cmd = new OpenSectionForRegistration()
            {
                Section = occurrence.Section
            };

            // TODO: Find a way to channel that command back through whatever application is using this domain,
            // so that the command comes back to this command handler and that any subsequent events are saved/published.
            //CommandProcessor.Process(cmd);
        }
    }
}

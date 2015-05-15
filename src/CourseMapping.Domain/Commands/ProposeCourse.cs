using SimpleCqrs.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMapping.Commands
{
    public class ProposeCourse : CommandWithAggregateRootId
    {
        public string CourseName { get; private set; }
        public string ProgramName { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ProposeCourse"/> class.
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="programName"></param>
        public ProposeCourse(string courseName, string programName)
        {
            CourseName = courseName;
            ProgramName = programName;
            AggregateRootId = Guid.NewGuid();
        }
    }
}

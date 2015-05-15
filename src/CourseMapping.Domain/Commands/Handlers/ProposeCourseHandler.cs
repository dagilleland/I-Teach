using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseMapping.Commands.Handlers
{
    class ProposeCourseHandler : CommandHandler<ProposeCourse>
    {
        private readonly IDomainRepository repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProposeCourseHandler"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public ProposeCourseHandler(IDomainRepository repository)
        {
            this.repository = repository;
        }
        public override void Handle(ProposeCourse command)
        {
            throw new NotImplementedException();
        }
    }
}

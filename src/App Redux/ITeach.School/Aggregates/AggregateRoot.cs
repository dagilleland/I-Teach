using CommonInfrastructure;
using ITeach.School.Commands;
using ITeach.School.DomainModel;
using ITeach.School.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.School.Aggregates
{
    public class AggregateRoot
        : IHandleCommand<AddCourse>
        , IHandleCommand<ScheduleCourse>
        , IApplyEvent<CourseAdded>
    {
        private Dictionary<string, Course> _CourseRepository = new Dictionary<string, Course>();
        private List<string> _CourseSections = new List<string>();
        public IEnumerable<object> Handle(AddCourse command)
        {
            var course = CreateCourse(command.CourseNumber, command.CourseName, command.Credits, command.TotalHours);
            // Check rules

            _CourseRepository.Add(command.CourseNumber, course);
            // Create event
            var result = new CourseAdded
            {
                CourseNumber = command.CourseNumber,
                CourseName = command.CourseName,
                Credits = command.Credits,
                TotalHours = command.TotalHours
            };

            return new object[] { result };
        }

        #region Factory Methods
        private Course CreateCourse(string courseNumber, string courseName, double credits, int hours)
        {
            var number = new CourseNumber(courseNumber);
            var name = new CourseName(courseName);
            var hoursa = new Hours(hours);
            var creditsa = new Credits(credits);
            return new Course(number, name, hoursa, creditsa);
        }
        #endregion

        public IEnumerable<object> Handle(ScheduleCourse command)
        {

            Term.StartMonth start;
            Enum.TryParse<Term.StartMonth>(command.Term, true, out start);
            var number = new CourseNumber(command.CourseNumber);
            var term = new Term(start, command.Year);
            var offering = new CourseOffering(number, term);
            List<object> events = new List<object>();
            events.Add( new CourseScheduled() { CourseOfferingId = offering.OfferingId });
            foreach (var section in command.Sections)
            {
                var courseSection = offering.OfferingId + "-" + section;
                _CourseSections.Add(courseSection);
                events.Add(new CourseSectionScheduled() { CourseOfferingId = offering.OfferingId, Section = section });
            }

            return events;
        }

        public void Apply(CourseAdded theEvent)
        {

        }
    }
}

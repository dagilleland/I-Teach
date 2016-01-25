using CommonInfrastructure;
using ITeach.CourseEvaluation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITeach.CourseEvaluation.DomainModel;
using ITeach.CourseEvaluation.Events;
namespace ITeach.CourseEvaluation.Aggregates
{
    public class AggregateRoot
        : IHandleCommand<ProvisionCourseEvaluation>
        , IHandleCommand<CreateDraftEvaluation>
        , IHandleCommand<AddEvaluationItem>
        , IHandleCommand<AddEvaluationGroup>
        , IHandleCommand<AddEvaluationGroupItem>
        , IHandleCommand<UseDraftEvaluationAsCurrent>
    {
        private Dictionary<string, Course> _Repository = new Dictionary<string, Course>();
        public IEnumerable<object> Handle(AddEvaluationItem command)
        {
            var events = new List<object>();

            // Load up the course
            var course = _Repository[command.CourseNumber];
            var item = new EvaluationItem(command.Name, command.Weight);
            // RULE: Business rule that there can be no duplicated items (items with the same name) in the whole evaluation
            course.AddEvaluation(item);

            return events;
        }

        public IEnumerable<object> Handle(AddEvaluationGroup command)
        {
            var events = new List<object>();

            // Load up the course
            var course = _Repository[command.CourseNumber];
            var group = new EvaluationGroup(command.Name, command.Pass);
            // RULE: Business rule that there can be no duplicated group names in the evaluation
            course.ProposeEvaluationGroup(group);
            // TODO: EVENT -> EvaluationGroupProposed

            return events;
        }

        public IEnumerable<object> Handle(AddEvaluationGroupItem command)
        {
            var events = new List<object>();

            // Load up the course
            var course = _Repository[command.CourseNumber];
            var item = new EvaluationItem(command.Name, command.Weight);
            // RULE: Business rule that there can be no duplicated items (items with the same name) in the whole evaluation

            course.AddEvaluationToGroup(command.GroupName, item);


            return events;
        }

        public IEnumerable<object> Handle(UseDraftEvaluationAsCurrent command)
        {
            var events = new List<object>();

            // Load up the course
            var course = _Repository[command.CourseNumber];
            course.ApproveDraftEvalation();

            return events;
        }

        public IEnumerable<object> Handle(ProvisionCourseEvaluation command)
        {
            Course course = new Course(command.CourseNumber);
            if (course.DraftEvaluation == null)
            {
                course.CreateDraftEvaluation(new Evaluation());
                // TODO: EVENT -> DraftEvaluationCreated
            }
            _Repository.Add(course.Number, course);

            var theEvent = new CourseEvaluationProvisioned
            {
                CourseNumber = course.Number
            };
            var another = new DraftEvaluationCreated
            {
                CourseNumber = course.Number
            };

            return new object[] { theEvent };
        }

        public IEnumerable<object> Handle(CreateDraftEvaluation command)
        {
            var events = new List<object>();

            // Load up the course
            var course = _Repository[command.CourseNumber];
            course.CreateDraftEvaluation(new Evaluation());

            return events;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.CourseEvaluation.Commands
{
    public class ProvisionCourseEvaluation
    {
        public string CourseNumber { get; set; }
    }
    public class CreateDraftEvaluation
    {
        public string CourseNumber { get; set; }
    }
    public class AddEvaluationItem
    {
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
    public class AddEvaluationGroupItem : AddEvaluationItem
    {
        public string GroupName { get; set; }
    }
    public class AddEvaluationGroup
    {
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public int? Pass { get; set; }
    }
    public class UseDraftEvaluationAsCurrent
    {
        public string CourseNumber { get; set; }
    }
}

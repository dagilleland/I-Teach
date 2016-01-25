using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.CourseEvaluation.Events
{
    public class CourseEvaluationProvisioned
    {
        public string CourseNumber { get; set; }
    }
    public class DraftEvaluationCreated
    {
        public string CourseNumber { get; set; }
    }
}

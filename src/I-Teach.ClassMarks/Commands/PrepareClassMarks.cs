using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Teach.ClassMarks.Commands
{
    public class PrepareClassMarks
    {
        public string CourseNumber { get; set; }
        public string CourseName { get; set; }
        public string Section { get; set; }
        public IList<PlannedEvaluation> Evaluations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrepareClassMarks"/> class.
        /// </summary>
        /// <param name="courseNumber"></param>
        /// <param name="courseName"></param>
        /// <param name="section"></param>
        /// <param name="evaluations"></param>
        public PrepareClassMarks(string courseNumber, string courseName, string section, params PlannedEvaluation[] evaluations)
        {
            CourseNumber = courseNumber;
            CourseName = courseName;
            Section = section;
            Evaluations = evaluations;
        }
    }
    public struct PlannedEvaluation
    {
        public string Name;
        public int RelativeWeight;
    }
}

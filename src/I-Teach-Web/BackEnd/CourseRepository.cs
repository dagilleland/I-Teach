using I_Teach.ViewModels.CourseAdministrationDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_Teach_Web.BackEnd
{
    public static class CourseRepository
    {
        public static Dictionary<string, Course> Courses = new Dictionary<string, Course>();
        public static Dictionary<Course, Tuple<List<EvaluationGroup>, List<EvaluationItem>>> CourseEvaluations = new Dictionary<Course,Tuple<List<EvaluationGroup>,List<EvaluationItem>>>();
    }
}
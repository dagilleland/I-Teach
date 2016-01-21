using I_Teach.ViewModels.CourseAdministrationDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace I_Teach_Web.BackEnd
{
    [DataObject]
    public static class PublicApi
    {
        #region Course Administration
        #region Queries
        public static List<Course> ListCurrentCourses()
        {
            return CourseRepository.Courses.Values.ToList();
        }
        public static Tuple<List<EvaluationGroup>, List<EvaluationItem>> GetEvaluationComponents(string courseNumber)
        {
            var egroups = new List<EvaluationGroup>();
            var eitems = new List<EvaluationItem>();
            var result = new Tuple<List<EvaluationGroup>, List<EvaluationItem>>(egroups, eitems);

            Course key = CourseRepository.Courses[courseNumber];
            if (CourseRepository.CourseEvaluations.ContainsKey(key))
                result = CourseRepository.CourseEvaluations[key];
            return result;
        }
        #endregion

        #region Commands
        public static void AddCourse(string number, string name)
        {
            CourseRepository.Courses.Add(number, new Course { Name = name, Number = number });
        }

        public static void SetEvaluationComponents(string courseNumber, Tuple<List<EvaluationGroup>, List<EvaluationItem>> courseEvaluation)
        {
            Course key = CourseRepository.Courses[courseNumber];
            if (CourseRepository.CourseEvaluations.ContainsKey(key))
                CourseRepository.CourseEvaluations[key] = courseEvaluation;
            else
                CourseRepository.CourseEvaluations.Add(key, courseEvaluation);
        }

        public static void RetireCourse(string number)
        {

        }
        #endregion
        #endregion
    }
}
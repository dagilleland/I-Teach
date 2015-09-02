using I_Teach.CoursePlanningCalendar.Commands;
using I_Teach.CoursePlanningCalendar.Fetch.Model;
using I_Teach.CoursePlanningCalendar.Web.AdHoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I_Teach.CoursePlanningCalendar.Web
{
    public class AppProxy
    {
        public I_Teach.SchoolApplication App { get; set; }
        public AppProxy()
        {
            App = I_Teach.SchoolApplication.Instance();
        }

        public static AppProxy Instance()
        {
            return new AppProxy();
        }

        public string ExceptionMessage { get; set; }

        public bool HasException { get; set; }

        #region Query Methods
        public IEnumerable<DraftPlanningCalendar> ListCalendars()
        {
            var calendars = App.PlanningCalendarRepository.ListDraftPlanningCalendars();

            return calendars;
        }

        public List<Course> ListCourses()
        {
            using (var context = new AdHocContext())
            {
                return context.Courses.ToList();
            }
        }

        public IEnumerable<Topic> ListTopics(Guid guid)
        {
            return App.PlanningCalendarRepository.ListTopics(guid);
        }

        public DraftPlanningCalendar GetCalendar(Guid selectedCalendar)
        {
            return App.PlanningCalendarRepository.FindDraftPlanningCalendar(selectedCalendar);
        }

        #endregion

        #region Command Methods
        public void CreateCourse(string name, string number, int totalHours, int weeks = 15)
        {
            using (var context = new AdHocContext())
            {
                context.Courses.Add(new Course() { Id = Guid.NewGuid(), Name = name, Number = number, TotalHours = totalHours, Weeks = weeks });
                context.SaveChanges();
            }
        }

        public void CreateCourseCalendar(Guid courseId)
        {
            try
            {
                using (var context = new AdHocContext())
                {
                    var course = context.Courses.Find(courseId);
                    var command = new CreatePlanningCalendar(course.Name, course.Number, course.TotalHours, 3);
                    App.Process(command);
                }
            }
            catch (Exception ex)
            {
                HasException = true;
                ExceptionMessage = ex.Message;
            }
        }

        public void ScheduleCourseCalendar(Guid calendarId, int year, string month)
        {
            try
            {
                SchedulePlanningCalendar command = new SchedulePlanningCalendar(calendarId, year, month);
                App.Process(command);
            }
            catch (Exception ex)
            {
                HasException = true;
                ExceptionMessage = ex.Message;
            }
        }

        public void AppendTopic(Guid calendarId, string title, string description, double duration)
        {
            try
            {
                AppendTopic command = new AppendTopic(calendarId, title, title, duration);
                App.Process(command);
            }
            catch (Exception ex)
            {
                HasException = true;
                ExceptionMessage = ex.Message;
            }
        }

        public void AppendEvaluation(Guid calendarId, string name, int weight, double duration)
        {
            try
            {
                AppendEvaluation command = new AppendEvaluation(calendarId, name, weight, duration);
                App.Process(command);
            }
            catch (Exception ex)
            {
                HasException = true;
                ExceptionMessage = ex.Message;
            }
        }

        public void AppendWorkPeriod(Guid calendarId, string name, double duration)
        {
            try
            {

            }
            catch (Exception ex)
            {
                HasException = true;
                ExceptionMessage = ex.Message;
            }
        }

        #endregion
    }
}
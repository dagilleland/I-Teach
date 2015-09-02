using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class CalendarCreated : AbstractEventWithId
    {
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public int TotalHours { get; set; }
        public int ClassesPerWeek { get; set; }
    }
}

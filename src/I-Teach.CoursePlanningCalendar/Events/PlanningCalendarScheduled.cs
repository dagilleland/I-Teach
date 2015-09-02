using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class PlanningCalendarScheduled
    {
        public Guid CalendarId { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
    }
}

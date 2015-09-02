using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class DurationChanged : AbstractEventWithId
    {
        public string CalendarItemName { get; set; }
        public double NewDuration { get; set; }
    }
}

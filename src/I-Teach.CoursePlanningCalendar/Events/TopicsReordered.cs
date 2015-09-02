using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class CalendarItemMoved : AbstractEventWithId
    {
        public string Title { get; set; }
        public int Position { get; set; }
    }
    public class SequenceChanged : AbstractEventWithId
    {
        public string[] Sequence { get; set; }
    }
}

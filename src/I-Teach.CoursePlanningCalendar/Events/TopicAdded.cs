using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class TopicAdded : AbstractEventWithId
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class TopicRemoved : AbstractEventWithId
    {
        public string Title { get; set; }
    }
}

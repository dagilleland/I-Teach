using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Events
{
    public class TopicReplaced : AbstractEventWithId
    {
        public string Title { get; set; }
        public string NewDescription { get; set; }
        public double NewDuration { get; set; }
        public int Sequence { get; set; }
    }
    
    [Obsolete("Phase out in favor or TopicReplaced")]
    public class TopicRenamed : AbstractEventWithId
    {
        public string Title { get; set; }
        public string NewTitle { get; set; }
    }
}

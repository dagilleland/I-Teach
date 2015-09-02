using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Domain
{
    class Topic : CalendarItem
    {
        public string Description { get; private set; }
        public Topic(Name title, string description, double duration) : base(title)
        {
            Description = description;
            Duration = new Duration(duration);
        }
        public Topic(Name title, string description, Duration duration) : base(title)
        {
            Description = description;
            Duration = duration;
        }
    }
}

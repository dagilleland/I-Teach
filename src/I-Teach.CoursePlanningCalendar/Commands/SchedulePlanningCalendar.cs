using CommonUtilities.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Commands
{
    public class SchedulePlanningCalendar : CommandWithAggregateRootId
    {
        public int Year { get; set; }
        public string Term { get; set; }

        public SchedulePlanningCalendar(Guid planningCalendarId, int year, string term)
        {
            Id = planningCalendarId;
            Year = year;
            Term = term;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Domain
{
    abstract class CalendarItem
    {
        public Name Name { get; private set; }
        public int Sequence { get; set; }
        public Duration Duration { get; set; }
        // TODO: Update constructor
        public CalendarItem(Name name)
        {
            Name = name;
        }
    }
    class Duration
    {
        public int quarterHourIncrements { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Duration"/> class.
        /// </summary>
        /// <param name="hours"></param>
        public Duration(double hours)
        {
            // TODO: Validation - right now, I'm just convertnig to nearest 1/4 hour increments
            int increments = (int)Math.Round(hours * 4, MidpointRounding.AwayFromZero) / 4;
            this.quarterHourIncrements = increments;
        }
    }
}

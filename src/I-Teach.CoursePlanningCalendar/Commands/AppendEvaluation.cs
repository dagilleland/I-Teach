using CommonUtilities.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Commands
{
    public class AppendEvaluation : CommandWithAggregateRootId
    {
        public string Title { get; set; }
        public int Weight { get; set; }
        public double Duration { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppendEvaluation"/> class.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="weight"></param>
        /// <param name="duration"></param>
        public AppendEvaluation(Guid aggregateRootId, string title, int weight, double duration)
        {
            Id = aggregateRootId;
            Title = title;
            Weight = weight;
            Duration = duration;
        }
    }

    public class ChangeDuration : CommandWithAggregateRootId
    {
        public string CalendarItemName { get; set; }
        public double NewDuration { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDuration"/> class.
        /// </summary>
        /// <param name="calendarItemName"></param>
        /// <param name="newDuration"></param>
        public ChangeDuration(Guid aggregateRootId, string calendarItemName, double newDuration)
        {
            Id = aggregateRootId;
            CalendarItemName = calendarItemName;
            NewDuration = newDuration;
        }
    }
}

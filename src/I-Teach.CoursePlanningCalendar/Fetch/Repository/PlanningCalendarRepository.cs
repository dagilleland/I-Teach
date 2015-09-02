using Edument.CQRS;
using I_Teach.CoursePlanningCalendar.Events;
using I_Teach.CoursePlanningCalendar.Fetch.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Fetch
{
    internal class PlanningCalendarRepository
        : IPlanningCalendarRepository
        , ISubscribeTo<CalendarCreated>
        , ISubscribeTo<PlanningCalendarScheduled>
        , ISubscribeTo<TopicAdded>
        , ISubscribeTo<TopicRemoved>
        , ISubscribeTo<TopicRenamed>
        , ISubscribeTo<SequenceChanged>
    {
        #region Constructor
        public PlanningCalendarRepository()
        {
        }
        #endregion

        #region IPlanningCalendar Implementations
        public DraftPlanningCalendar FindDraftPlanningCalendar(Guid id)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                return context.DraftPlanningCalendars.Find(id);
            }
        }

        public IEnumerable<DraftPlanningCalendar> ListDraftPlanningCalendars()
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                return context.DraftPlanningCalendars.ToList();
            }
        }

        // TODO: I forsee a need to re-do this to show all calendar items...
        public IEnumerable<Topic> ListTopics(Guid draftPlanningCalendarId)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                var topics = context.Topics.Where(x => x.PlanningCalendarId == draftPlanningCalendarId);
                return topics.ToList().OrderBy(x=>x.Sequence);
            }
        }
        #endregion

        #region Event Subscribers - persisting to the Read database
        public void Handle(CalendarCreated e)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                DraftPlanningCalendar calendar = new DraftPlanningCalendar()
                {
                    Id = e.Id,
                    CourseName = e.CourseName,
                    CourseNumber = e.CourseNumber,
                    TotalHours = e.TotalHours,
                    ClassesPerWeek = e.ClassesPerWeek
                };
                context.DraftPlanningCalendars.Add(calendar);
                context.SaveChanges();
            }
        }

        public void Handle(PlanningCalendarScheduled e)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                var calendar = context.DraftPlanningCalendars.Find(e.CalendarId);
                calendar.Year = e.Year;
                calendar.Month = e.Month;

                context.Entry(calendar).Property(p => p.Year).IsModified = true;
                context.Entry(calendar).Property(p => p.Month).IsModified = true;
                context.SaveChanges();
            }
        }

        public void Handle(TopicAdded e)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                Topic topic = new Topic()
                {
                    PlanningCalendarId = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Duration = e.Duration
                };
                context.Topics.Add(topic);
                context.SaveChanges();
            }
        }

        public void Handle(TopicRemoved e)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                var existing = context.Topics.Single(x => x.Title == e.Title && x.PlanningCalendarId == e.Id);
                context.Topics.Remove(existing);
                context.SaveChanges();
            }
        }

        public void Handle(TopicRenamed e)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                var topicToRename = context.Topics.Single(x => x.Title == e.Title && x.PlanningCalendarId == e.Id);
                topicToRename.Title = e.NewTitle;
                context.Entry(topicToRename).Property(x => x.Title).IsModified = true;
                context.SaveChanges();
            }
        }

        public void Handle(SequenceChanged e)
        {
            using (var context = new ReadModelDataStore(About.ConnectionStringName))
            {
                for (int index = 0; index < e.Sequence.Length; index++)
                {
                    string title = e.Sequence[index];
                    var topicToResequence = context.Topics.SingleOrDefault(x => x.Title == title && x.PlanningCalendarId == e.Id);
                    if (topicToResequence != null)
                    {
                        topicToResequence.Sequence = index;
                        context.Entry(topicToResequence).Property(x => x.Sequence).IsModified = true;
                    }
                }
                context.SaveChanges();
            }
        }
        #endregion
    }
}

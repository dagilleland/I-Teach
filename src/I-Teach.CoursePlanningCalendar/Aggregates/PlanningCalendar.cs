using Edument.CQRS;
using I_Teach.CoursePlanningCalendar.Commands;
using I_Teach.CoursePlanningCalendar.Domain;
using I_Teach.CoursePlanningCalendar.Events;
using I_Teach.CoursePlanningCalendar.Fetch;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace I_Teach.CoursePlanningCalendar.Aggregates
{
    class PlanningCalendar
        : Aggregate
        , IHandleCommand<CreatePlanningCalendar>
            , IApplyEvent<CalendarCreated>
        , IHandleCommand<SchedulePlanningCalendar>
            , IApplyEvent<PlanningCalendarScheduled>
        , IHandleCommand<AppendTopic>
            , IApplyEvent<TopicAdded>
            , IApplyEvent<SequenceChanged>
        , IHandleCommand<RemoveTopic>
            , IApplyEvent<TopicRemoved>
        , IHandleCommand<ReplaceTopic>
            , IApplyEvent<ReplaceTopic>
        , IHandleCommand<RenameTopic>
            , IApplyEvent<TopicRenamed>
        , IHandleCommand<MoveCalendarItem>
            , IApplyEvent<CalendarItemMoved>
        , IHandleCommand<AppendEvaluation>
            , IApplyEvent<EvaluationAdded>
        , IHandleCommand<ChangeDuration>
            , IApplyEvent<DurationChanged>
    {
        IPlanningCalendarRepository ReadModel = new PlanningCalendarRepository();

        #region Domain Behaviour
        private OrderedDictionary CalendarItems; // TODO: Probably can find/make a better option than an ordered dictionary. Ideally, I would like an OrderedDictionary<T>...

        private string[] GetCalendarItemsSequence()
        {
            return CalendarItems.Keys.Cast<string>().ToArray();
        }
        private bool TopicExists(string name)
        {
            return CalendarItems[name] != null;
        }

        private void AppendCalendarItem(Topic item)
        {
            CalendarItems.Add(item.Name.ToString(), item);
        }

        private void AppendCalendarItem(EvaluationComponent item)
        {
            CalendarItems.Add(item.Name.ToString(), item);
        }

        private void AppendCalendarItem(WorkPeriod item)
        {
            CalendarItems.Add(item.Name.ToString(), item);
        }

        private int GetIndexByName(string name)
        {
            int index = 0;
            while (((CalendarItem)CalendarItems[index]).Name != name)
                index++;
            return index;
        }

        private void ChangeCalendarItem(string keyName, string newDescription, double newDuration)
        {
            int index = GetIndexByName(keyName);
            RemoveCalendarItem(keyName);
            CalendarItems.Insert(index, keyName, new Topic((Name)keyName, newDescription, newDuration));
        }
        private void RemoveCalendarItem(string title)
        {
            CalendarItems.Remove(title);
        }
        private void RenameCalendarItem(string title, string newTitle)
        {
            var index = GetIndexByName(title);
            var item = CalendarItems[title] as Topic;
            CalendarItems.Insert(index, newTitle, new Topic((Name)newTitle, item.Description, item.Duration));
        }
        private void RePositionTopic(string title, int position)
        {
            // TODO: I know the logic here is a bit flakey, but I'll address it when I run into problems.....
            var item = CalendarItems[title] as Topic;
            CalendarItems.RemoveAt(GetIndexByName(title));
            CalendarItems.Insert(position, title, item); 
        }
        #endregion

        // TODO: Consider moving this into the base class.....
        private void TryToDo<T>(Action<T> eventProcessor, T eventObj)
        {
            try 
	        {
                eventProcessor(eventObj);
	        }
	        catch (Exception e)
	        {
                throw new InvalidOperationException("Unable to process the command - the resulting event cannot be applied: " + e.Message, e);
	        }
        }

        #region Command Handlers
        public IEnumerable Handle(CreatePlanningCalendar c)
        {
            if (c.Id == Guid.Empty)
                throw new InvalidOperationException("Draft Planning Calendar already exists");
            var existing = ReadModel.ListDraftPlanningCalendars();
            foreach (var item in existing)
            {
                if (c.CourseName == item.CourseName)
                    throw new InvalidOperationException("Draft Planning Calendar with the same course name already exists");
                if (c.CourseNumber == item.CourseNumber)
                    throw new InvalidOperationException("Draft Planning Calendar with the same course number already exists");
            }

            // Generate events
            CalendarCreated newCalendarCreated = new CalendarCreated()
                            {
                                Id = c.Id,
                                CourseName = c.CourseName,
                                CourseNumber = c.CourseNumber,
                                TotalHours = c.TotalHours,
                                ClassesPerWeek = c.ClassesPerWeek
                            };
            // Process the events
            TryToDo<CalendarCreated>(Apply, newCalendarCreated);

            // Return applied event for other subscribers
            yield return newCalendarCreated;
        }

        public IEnumerable Handle(SchedulePlanningCalendar c)
        {
            // Generate event
            PlanningCalendarScheduled theEvent = new PlanningCalendarScheduled()
            {
                CalendarId = c.Id,
                Year = c.Year,
                Month = c.Term
            };
            // Process the event


            // Return the event for other subscribers
            yield return theEvent;
        }

        public IEnumerable Handle(AppendTopic c)
        {
            if (c.Id == Guid.Empty)
                throw new InvalidOperationException("Draft Planning Calendar has not been loaded");
            if (c.Id != Id)
                throw new InvalidOperationException("Cannot Append Topic - Wrong planning calendar");

            if (TopicExists(c.Title))
                throw new InvalidOperationException("Cannot Append Topic - An item by that name already exists on the calendar");

            // Generate and process events
            TopicAdded newTopicAdded = new TopicAdded()
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Duration = c.Duration
            };
            TryToDo<TopicAdded>(Apply, newTopicAdded);

            SequenceChanged newSequenceChanged = new SequenceChanged()
            {
                Id = c.Id,
                Sequence = GetCalendarItemsSequence()
            };
            TryToDo<SequenceChanged>(Apply, newSequenceChanged);

            // Return applied event for other subscribers
            yield return newTopicAdded;
            yield return newSequenceChanged;
        }

        public IEnumerable Handle(RemoveTopic c)
        {
            yield return new TopicRemoved()
            {
                Id = c.Id,
                Title = c.Title
            };
            yield return new SequenceChanged()
            {
                Id = c.Id,
                Sequence = GetCalendarItemsSequence()
            };
        }

        public IEnumerable Handle(ReplaceTopic c)
        {
            yield return new TopicReplaced()
            {
                Id = c.Id,
                Title = c.Title,
                NewDescription = c.NewDescription,
                NewDuration = c.NewDuration,
                Sequence = c.Sequence
            };
        }

        public IEnumerable Handle(RenameTopic c)
        {
            yield return new TopicRenamed()
            {
                Id = c.Id,
                Title = c.Title,
                NewTitle = c.NewTitle
            };
            yield return new SequenceChanged()
            {
                Id = c.Id,
                Sequence = GetCalendarItemsSequence()
            };
        }

        public IEnumerable Handle(MoveCalendarItem c)
        {
            // 0) Validate that the command can be processed

            // 1) Create the event object
            CalendarItemMoved newTopicMoved = new CalendarItemMoved()
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Position = c.NewPosition
                        };

            // 2) Process the command by trying to do the event
            TryToDo<CalendarItemMoved>(Apply, newTopicMoved);

            // 3) Return the event (and any ancillary events)
            yield return newTopicMoved;
            yield return new SequenceChanged()
            {
                Id = c.Id,
                Sequence = GetCalendarItemsSequence()
            };
        }

        public IEnumerable Handle(AppendEvaluation c)
        {
            // Validation of command particulars

            // Generate and process events
            EvaluationAdded newEvaluationAdded = new EvaluationAdded()
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Weight = c.Weight,
                            Duration = c.Duration
                        };
            TryToDo<EvaluationAdded>(Apply, newEvaluationAdded);

            SequenceChanged newSequenceChanged = new SequenceChanged()
            {
                Id = c.Id,
                Sequence = GetCalendarItemsSequence()
            };
            TryToDo<SequenceChanged>(Apply, newSequenceChanged);

            // Return applied event for other subscribers
            yield return newEvaluationAdded;
            yield return newSequenceChanged;
        }

        public IEnumerable Handle(ChangeDuration c)
        {
            // Validation of command particulars

            // Generate and process events
            DurationChanged changeEvent = new DurationChanged()
            {
                Id = c.Id,
                CalendarItemName = c.CalendarItemName,
                NewDuration = c.NewDuration
            };

            // Return applied events for other subscribers
            yield return changeEvent;
        }
        #endregion

        #region Apply Events
        public void Apply(CalendarCreated e)
        {
            CalendarItems = new OrderedDictionary();
        }

        public void Apply(TopicAdded e)
        {
            AppendCalendarItem(new Topic((Name) e.Title, e.Description, e.Duration));
        }

        public void Apply(TopicRemoved e)
        {
            RemoveCalendarItem(e.Title);
        }

        public void Apply(ReplaceTopic e)
        {
            ChangeCalendarItem(e.Title, e.NewDescription, e.NewDuration);
        }

        public void Apply(TopicRenamed e)
        {
            RenameCalendarItem(e.Title, e.NewTitle);
        }

        public void Apply(CalendarItemMoved e)
        {
            RePositionTopic(e.Title, e.Position);
        }

        public void Apply(SequenceChanged e)
        {
            // no-op...
        }

        public void Apply(PlanningCalendarScheduled e)
        {
            throw new NotImplementedException();
        }

        public void Apply(EvaluationAdded e)
        {
            AppendCalendarItem(new EvaluationComponent((Name)e.Title));
        }

        public void Apply(DurationChanged e)
        {
            // no-op...
        }
        #endregion
    }
}

using Edument.CQRS;
using I_Teach.CoursePlanningCalendar.Commands;
using I_Teach.CoursePlanningCalendar.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy;
using Ploeh.AutoFixture; // needed for the AutoFixture extension method .Create()
using Xunit;
using Xunit.Extensions;
using OM = I_Teach.CoursePlanningCalendar.Specs.Helpers.ObjectMother;
using I_Teach.CoursePlanningCalendar.Fetch.Model;

namespace I_Teach.CoursePlanningCalendar.Specs.Stories.DraftCalendars
{
    [Story(AsA = Actor.CourseCoordinator,
            IWant = "I Want to edit the list of topics",
            SoThat = "So as to plan the topics to cover in class")]
    public class Edit_Topics
        : Abstract_Story
        , ISubscribeTo<TopicAdded>
        , ISubscribeTo<ReplaceTopic>
        , ISubscribeTo<TopicRenamed>
        , ISubscribeTo<TopicRemoved>
        , ISubscribeTo<CalendarItemMoved>
        , ISubscribeTo<SequenceChanged>
    {
        public Edit_Topics()
        {
        }

        #region Scenarios
        private TopicAdded Actual_TopicAdded_Event;
        [Theory, AutoRollback]
        [InlineData(0)]
        [InlineData(4)]
        [Trait("Context", "Acceptance Test")]
        public void Add_a_topic(int existingTopicCount)
        {
            string title = "Lorem Ipsum";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tristique.";
            int duration = 2;
            this.Given(_=> GivenADraftCalendarHasBeenCreated())
                .And(_=>PreviousTopicsWereAppended(existingTopicCount))
                .And(_=>AnAddTopicCommand(title, description, duration))
                .When(_=>AddingTheTopic())
                .Then(_=>ThenATopicAddedEventOccurs())
                .And(_ => ThenASequenceChangedEventOccurs())
                .And(_ => TheTopicAppearsAsTheLastTopicOnTheCalendar(title, description, duration))
                .BDDfy();
        }

        private ReplaceTopic Actual_TopicChanged_Event;
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Change_a_topic()
        {
            string title = "Lorem Ipsum";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tristique.";
            int duration = 2;
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_ => AnAddTopicCommand(title, description, duration))
                .And(_ => AddingTheTopic())
                .When(_ => ChangingTheTopic(title, description, duration))
                .And(_=> ThenATopicChangedEventOccurs())
                .And(_ => TheTopicExistsInTheCalendar(title, description, duration))
                .BDDfy();
        }

        private TopicRenamed Actual_TopicRenamed_Event;
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Rename_a_topic()
        {
            string title = "Lorem Ipsum";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tristique.";
            int duration = 2;
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_ => AnAddTopicCommand(title, description, duration))
                .And(_ => AddingTheTopic())
                .When(_ => RenamingTheTopic(title, "New " + title))
                .And(_ => ThenATopicRenamedEventOccurs())
                .And(_ => ThenASequenceChangedEventOccurs())
                .And(_ => TheTopicExistsInTheCalendar("New " + title, description, duration))
                // TODO: The following is more descriptive & granular
                //.And(_=>TheTopicDescriptionIs(description))
                //.And(_=>TheTopicDurationIs(duration))
                .BDDfy();
        }

        private TopicRemoved Actual_TopicRemoved_Event;
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Remove_a_topic()
        {
            string title = "Lorem Ipsum";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tristique.";
            int duration = 2;
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_ => AnAddTopicCommand(title, description, duration))
                .And(_ => AddingTheTopic())
                .When(_ => RemovingTheTopic())
                .Then(_=>ThenARemoveTopicEventOccurs())
                .And(_ => ThenASequenceChangedEventOccurs())
                .And(_ => TheTopicDoesNotAppearOnTheCalendar(title))
                .BDDfy();
        }

        private CalendarItemMoved Actual_TopicMoved_Event;
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Reorder_topics()
        {
            string title = "Lorem Ipsum";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tristique.";
            int duration = 2;
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_=>PreviousTopicsWereAppended(5))
                .And(_ => AnAddTopicCommand(title, description, duration))
                .And(_=>AddingTheTopic())
                .When(_ => MovingTheTopicToPosition(title, 3))
                .Then(_=>ThenATopicMovedEventOccurs())
                .And(_ => ThenASequenceChangedEventOccurs())
                .And(_ => TheSequenceChangedEventShowsTheTopicInTheRightPosition(title, 3))
                .And(_ => TheTopicAppearsInPosition(title, 3))
                .BDDfy();
        }

        private SequenceChanged Actual_SequenceChanged_Event;
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void AddASetOfTopics()
        {
            string[] sequence = { "First", "Second", "Third", "Fourth", "Fifth" };
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .When(_ => AddingASetOfTopics(sequence))
                .Then(_ => ThenASequenceChangedEventOccurs())
                .And(_ => TheSequenceOfTopicsIsCorrect(sequence))
                .BDDfy();
        }

        public void TBA() { throw new NotImplementedException(); }

        #region Alternate Scenarios
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Reject_Duplicate_Topics()
        {
            string title = "Lorem Ipsum";
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent tristique.";
            int duration = 2;
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_ => AnAddTopicCommand(title, description, duration))
                .And(_ => AddingTheTopic())
                .When(_=>AddingTheTopicWithExpectedException())
                .Then(_ => ThenTheExpectedExceptionIsGenerated<InvalidOperationException>())
                .BDDfy();
        }

        #endregion
        #endregion

        #region Givens

        private void AnAddTopicCommand(string title, string description, int duration)
        {
            Command = OM.Commands.AppendTopicCommand(AggregateRootId, title, description, duration);
        }
        #endregion

        #region Whens
        private void AddingTheTopic()
        {
            sut.Process(Command as AppendTopic);
        }
        private void AddingASetOfTopics(string[] sequence)
        {
            foreach(var title in sequence)
                sut.Process(OM.Commands.AppendTopicCommand(AggregateRootId, title));
        }
        private void ChangingTheTopic(string title, string description, int duration)
        {
            Command = OM.Commands.ChangeTopicCommand(AggregateRootId, title, description, duration);
            sut.Process(Command as ReplaceTopic);
        }
        private void RenamingTheTopic(string title, string newTitle)
        {
            Command = OM.Commands.RenameTopicCommand(AggregateRootId, title, newTitle);
            sut.Process(Command as RenameTopic);
        }
        private void RemovingTheTopic()
        {
            var appendCommand = Command as AppendTopic;
            Command = OM.Commands.RemoveTopicCommand(AggregateRootId, appendCommand.Title, appendCommand.Description, appendCommand.Duration);
            sut.Process(Command as RemoveTopic);
        }
        private void MovingTheTopicToPosition(string title, int position)
        {
            Command = OM.Commands.MoveTopicCommand(AggregateRootId, title, position);
            sut.Process(Command as MoveCalendarItem);
        }
        private void AddingTheTopicWithExpectedException()
        {
            ExecuteActionThatThrows(() => AddingTheTopic());
        }
        #endregion

        #region Thens
        private void TheTopicExistsInTheCalendar(string title, string description, int duration)
        {
            var topics = sut.PlanningCalendarRepository.ListTopics(AggregateRootId);

            foreach (var item in topics)
            {
                Assert.Equal(title, item.Title);
                Assert.Equal(description, item.Description);
                Assert.Equal(duration, item.Duration);
            }
        }

        private void TheTopicAppearsInPosition(string title, int position)
        {
            var topics = sut.PlanningCalendarRepository.ListTopics(AggregateRootId);
            var topic = topics.ElementAtOrDefault(position);
            Assert.NotNull(topic);
            Assert.Equal(title, topic.Title);
        }

        private void TheTopicAppearsAsTheLastTopicOnTheCalendar(string title, string description, int duration)
        {
            var topics = sut.PlanningCalendarRepository.ListTopics(AggregateRootId);
            var actual = topics.LastOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(AggregateRootId, actual.PlanningCalendarId);
            Assert.Equal(title, actual.Title);
            Assert.Equal(description, actual.Description);
            Assert.Equal(duration, actual.Duration);
        }

        private void TheTopicDoesNotAppearOnTheCalendar(string title)
        {
            var topics = sut.PlanningCalendarRepository.ListTopics(AggregateRootId);

            foreach (var item in topics)
            {
                Assert.NotEqual(title, item.Title);
            }
        }
        private void TheSequenceOfTopicsIsCorrect(string[] sequence)
        {
            Assert.Equal(sequence, Actual_SequenceChanged_Event.Sequence);
        }
        private void TheSequenceChangedEventShowsTheTopicInTheRightPosition(string title, int position)
        {
            Assert.Equal(title, Actual_SequenceChanged_Event.Sequence[position]);
        }
        #endregion


        #region Event Checks
        private void ThenATopicAddedEventOccurs()
        {
            Assert.NotNull(Actual_TopicAdded_Event);
        }
        private void ThenATopicChangedEventOccurs()
        {
            Assert.NotNull(Actual_TopicChanged_Event);
        }
        private void ThenATopicRenamedEventOccurs()
        {
            Assert.NotNull(Actual_TopicRenamed_Event);
        }
        private void ThenARemoveTopicEventOccurs()
        {
            Assert.NotNull(Actual_TopicRemoved_Event);
        }
        private void ThenATopicMovedEventOccurs()
        {
            Assert.NotNull(Actual_TopicMoved_Event);
        }
        private void ThenASequenceChangedEventOccurs()
        {
            Assert.NotNull(Actual_SequenceChanged_Event);
        }
        #endregion


        #region Event Subscribers
        public void Handle(TopicAdded e)
        {
            Actual_TopicAdded_Event = e;
        }

        public void Handle(ReplaceTopic e)
        {
            Actual_TopicChanged_Event = e;
        }

        public void Handle(TopicRemoved e)
        {
            Actual_TopicRemoved_Event = e;
        }

        public void Handle(CalendarItemMoved e)
        {
            Actual_TopicMoved_Event = e;
        }

        public void Handle(TopicRenamed e)
        {
            Actual_TopicRenamed_Event = e;
        }

        public void Handle(SequenceChanged e)
        {
            Actual_SequenceChanged_Event = e;
        }
        #endregion
    }
}

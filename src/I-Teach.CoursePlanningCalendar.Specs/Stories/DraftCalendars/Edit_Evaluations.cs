using I_Teach.CoursePlanningCalendar.Commands;
using I_Teach.CoursePlanningCalendar.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy;
using Xunit;
using Xunit.Extensions;
using Ploeh.AutoFixture; // needed for the AutoFixture extension method .Create()
using OM = I_Teach.CoursePlanningCalendar.Specs.Helpers.ObjectMother;
using Edument.CQRS;

namespace I_Teach.CoursePlanningCalendar.Specs.Stories.DraftCalendars
{
    [Story(AsA=Actor.Instructor,
           IWant="I Want to change the duration of an item on the calendar",
           SoThat="So as to adjust the schedule to allot a more accurate time for a topic, evaluation or work period")]
    public class Change_Duration
        : Abstract_Story
        , ISubscribeTo<DurationChanged>
    {
        public DurationChanged Actual_DurationChanged_Event { get; set; }
        public void Handle(DurationChanged e)
        {
            Actual_DurationChanged_Event = e;
        }

        //private I_Teach.SchoolApplication sut;
        [Theory, AutoRollback]
        [InlineData(1.5, 2.0)]
        [InlineData(2.0, 1.0)]
        public void Change_Topic_Duration(double originalDuration,double  newDuration)
        {
            string topicName = "A topic";
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_ => ATopicWasAppended(topicName, originalDuration))
                .And(_ => AChangeDurationCommand(topicName, newDuration))
                .When(_ => ChangingTheDuration())
                .Then(_ => ThenADurationChangedEventOccurs())
                .BDDfy();
        }
        private void ATopicWasAppended(string topicName, double originalDuration)
        {
            AppendTopic topic = OM.Commands.AppendTopicCommand(AggregateRootId, topicName, "", originalDuration);
            sut.Process(topic);
        }
        private void AChangeDurationCommand(string topicName, double newDuration)
        {
            Command = new ChangeDuration(AggregateRootId, topicName, newDuration);
        }
        private void ChangingTheDuration()
        {
            sut.Process(Command as ChangeDuration);
        }
        private void ThenADurationChangedEventOccurs()
        {
            Assert.NotNull(Actual_DurationChanged_Event);
        }

    }

    [Story(AsA = Actor.CourseCoordinator,
            IWant = "I Want to assign dates to evaluation components",
            SoThat = "So as to prepare students for evaluations")]
    public class Edit_Evaluations 
        : Abstract_Story
        , ISubscribeTo<EvaluationAdded>
        , ISubscribeTo<SequenceChanged>
    {
        //private I_Teach.SchoolApplication sut;
        public EvaluationAdded Actual_EvaluationAdded_Event { get; set; }
        public SequenceChanged Actual_SequenceChanged_Event { get; set; }
        //public Edit_Evaluations()
        //{
        //    sut = SchoolApplication.Instance();
        //}

        //Add an evaluation component
        //Change an evaluation component
        //Remove an evaluation component
        [Theory, AutoRollback]
        [InlineData(0, 0)]
        [InlineData(0, 2)]
        [InlineData(2, 0)]
        [InlineData(3, 1)]
        [Trait("Context", "Acceptance Test")]
        public void Add_an_evaluation(int existingTopicCount, int existingEvaluationCount)
        {
            this.Given(_ => GivenADraftCalendarHasBeenCreated())
                .And(_=>PreviousTopicsWereAppended(existingTopicCount))
                .And(_=>PreviousEventsWereAppended(existingEvaluationCount))
                .And(_=>AnAddEvaluationCommand("Major Quiz"))
                .When(_=>AddingTheEvaluation())
                .Then(_=>ThenAnEvaluationAddedEventOccurs())
                .And(_=>ThenASequenceChangedEventOccurs())
                .BDDfy();
        }

        //Add an evaluation component
        //Change an evaluation component
        //Remove an evaluation component
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void SCENARIO()
        {
            this.Given(_ => TBA())
                .BDDfy();
        }

        public void TBA() { throw new NotImplementedException(); }
        private void AnAddEvaluationCommand(string title)
        {
            Command = OM.Commands.AppendEvaluationCommand(AggregateRootId, title);
            Assert.NotNull(Command);
        }
        private void AddingTheEvaluation()
        {
            sut.Process(Command as AppendEvaluation);
        }
        private void ThenAnEvaluationAddedEventOccurs()
        {
            Assert.NotNull(Actual_EvaluationAdded_Event);
        }
        private void ThenASequenceChangedEventOccurs()
        {
            Assert.NotNull(Actual_SequenceChanged_Event);
        }

        public void Handle(EvaluationAdded e)
        {
            Actual_EvaluationAdded_Event = e;
        }

        public void Handle(SequenceChanged e)
        {
            Actual_SequenceChanged_Event = e;
        }
    }
}

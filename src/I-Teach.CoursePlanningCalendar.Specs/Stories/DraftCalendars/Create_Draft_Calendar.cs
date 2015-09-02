using CommonUtilities.Domain.Commands;
using Edument.CQRS;
using I_Teach.CoursePlanningCalendar.Commands;
using I_Teach.CoursePlanningCalendar.Events;
using I_Teach.CoursePlanningCalendar.Fetch;
using I_Teach.CoursePlanningCalendar.Fetch.Model;
using I_Teach.CoursePlanningCalendar.Specs.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy;
using Xunit;
using Xunit.Extensions;
using OM = I_Teach.CoursePlanningCalendar.Specs.Helpers.ObjectMother;

namespace I_Teach.CoursePlanningCalendar.Specs.Stories.DraftCalendars
{
    [Story(AsA = Actor.CourseCoordinator,
            IWant = "I Want to create a draft planning calendar for a course",
            SoThat = "So as to have a working copy of a planning calendar for editing")]
    public class Create_Draft_Calendar : Abstract_Story, ISubscribeTo<CalendarCreated>
    {
        private CalendarCreated ExpectedCalendarCreatedEvent;
        private CalendarCreated ActualCalendarCreatedEvent;

        public Create_Draft_Calendar()
        {
        }

        #region Scenarios
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Create_a_blank_draft_calendar()
        {
            this.Given(_ => GivenACreatePlanningCalendarCommand())
                .When(_ => WhenICreateANewDraftCalendar())
                .Then(_ => ThenADraftCalendarCreatedEventOccurs())
                .And(_ => ThenIHaveAReferenceToTheDraftCalendar())
                .And(_ =>ThenICanRetrieveTheDraftCalendar())
                .And(_=>ThenTheCalendarHasTheNameAndNumber())
                .BDDfy();
        }

        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Create_a_draft_from_an_existing_calendar()
        {
            // TODO: [Theory]
            //          From an approved Master calendar of a previous term
            //          From an instructor's adjusted calendar of a previous term
            this.Given(_ => TBA())
                .BDDfy();
        }

        #region Alternate Scenarios
        // TODO: Reject_Creating_Draft_Calendar_With_Duplicate_Name
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Reject_Creating_Draft_Calendar_With_Duplicate_Name()
        {
            this.Given(_ => GivenADraftPlanningCalendarWithTheSameCourseNameAlreadyExists())
                .And(_ => GivenACreatePlanningCalendarCommand())
                .When(_=>WhenICreateANewDraftCalendarWithExpectedException())
                .Then(_ => ThenTheExpectedExceptionIsGenerated<InvalidOperationException>())
                .BDDfy();
        }
        // TODO: Reject_Creating_Draft_Calendar_With_Duplicate_Number
        #endregion

        public void TBA() { throw new NotImplementedException(); }
        #endregion

        #region Event Subscribers
        public void Handle(CalendarCreated e)
        {
            ActualCalendarCreatedEvent = e;
        }
        #endregion

        #region Givens
        private void GivenACreatePlanningCalendarCommand()
        {
            Command = OM.Commands.CreatePlanningCalendar();
        }
        private void GivenADraftPlanningCalendarWithTheSameCourseNameAlreadyExists()
        {
            sut.ProcessInTurn(OM.Commands.CreatePlanningCalendar());
        }
        #endregion

        #region Whens
        public void WhenICreateANewDraftCalendar()
        {
            sut.Process(Command as CreatePlanningCalendar);
        }
        private void WhenICreateANewDraftCalendarWithExpectedException()
        {
            ExecuteActionThatThrows(() => WhenICreateANewDraftCalendar());
        }
        #endregion

        #region Thens
        private void ThenADraftCalendarCreatedEventOccurs()
        {
            Assert.NotNull(ActualCalendarCreatedEvent);
        }

        private void ThenIHaveAReferenceToTheDraftCalendar()
        {
            Assert.NotEqual(Guid.Empty, Command.Id);
        }
        private void ThenICanRetrieveTheDraftCalendar()
        {
            DraftPlanningCalendar actualCalendar = GetActualCalendar();
            Assert.NotNull(actualCalendar);
        }
        private DraftPlanningCalendar GetActualCalendar()
        {
            DraftPlanningCalendar actualCalendar = sut.PlanningCalendarRepository.FindDraftPlanningCalendar(Command.Id);
            return actualCalendar;
        }
        private void ThenTheCalendarHasTheNameAndNumber()
        {
            var calendar = GetActualCalendar();
            var expected = Command as CreatePlanningCalendar;
            Assert.Equal(expected.CourseName, calendar.CourseName);
            Assert.Equal(expected.CourseNumber, calendar.CourseNumber);
        }
        #endregion
    }
}

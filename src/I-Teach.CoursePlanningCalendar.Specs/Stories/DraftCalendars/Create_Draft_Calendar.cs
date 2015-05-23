using CommonUtilities.Domain.Commands;
using Edument.CQRS;
using I_Teach.CoursePlanningCalendar.Commands;
using I_Teach.CoursePlanningCalendar.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using TestStack.BDDfy;
using Xunit;
using Xunit.Extensions;

namespace I_Teach.CoursePlanningCalendar.Specs.Stories.DraftCalendars
{
    [Story(AsA = Actor.CourseCoordinator,
            IWant = "I Want to create a draft planning calendar for a course",
            SoThat = "So as to have a working copy of a planning calendar for editing")]
    public class Create_Draft_Calendar : ISubscribeTo<CalendarCreated>
    {
        private I_Teach.SchoolApplication sut;
        private CommandWithAggregateRootId Command;
        private CalendarCreated ExpectedCalendarCreatedEvent;
        private CalendarCreated ActualCalendarCreatedEvent;

        public Create_Draft_Calendar()
        {
            sut = SchoolApplication.Instance(this);
        }

        #region Scenarios
        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Create_a_blank_draft_calendar()
        {
            string courseName = "Enterprise Application Programming",
                   courseNumber = "EAP 205";
            this.Given(_ => GivenACreatePlanningCalendarCommand(courseName, courseNumber))
                .When(_ => WhenICreateANewDraftCalendar())
                .Then(_ => ThenADraftCalendarCreatedEventOccurs())
                .And(_ => ThenIHaveAReferenceToTheDraftCalendar())
                .And(_=>ThenTheCalendarHasTheNameAndNumber(courseName, courseNumber))
                .BDDfy();
        }

        [Fact, AutoRollback]
        [Trait("Context", "Acceptance Test")]
        public void Create_a_draft_from_an_existing_calendar()
        {
            this.Given(_ => TBA())
                .BDDfy();
        }

        public void TBA() { throw new NotImplementedException(); }
        #endregion

        #region Event Subscribers
        public void Handle(CalendarCreated e)
        {
            ActualCalendarCreatedEvent = e;
        }
        #endregion

        #region Givens
        private void GivenACreatePlanningCalendarCommand(string courseName, string courseNumber)
        {
            Command = new CreatePlanningCalendar(courseName, courseNumber);
        }
        #endregion

        #region Whens
        private void WhenICreateANewDraftCalendar()
        {
            sut.Process(Command as CreatePlanningCalendar);
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
        private void ThenTheCalendarHasTheNameAndNumber(string courseName, string courseNumber)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}

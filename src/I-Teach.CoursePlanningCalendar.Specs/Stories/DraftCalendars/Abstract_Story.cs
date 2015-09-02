using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUtilities.Domain.Commands;
using I_Teach.CoursePlanningCalendar.Specs.Helpers;
using Ploeh.AutoFixture; // needed for the AutoFixture extension method .Create()
using OM = I_Teach.CoursePlanningCalendar.Specs.Helpers.ObjectMother;
using System.Collections;
using Xunit;

namespace I_Teach.CoursePlanningCalendar.Specs.Stories.DraftCalendars
{
    public class Abstract_Story
    {
        protected Exception ActualExeption;
        protected I_Teach.SchoolApplication sut;
        protected CommandWithAggregateRootId Command;
        protected Guid AggregateRootId;
        public Abstract_Story()
        {
            sut = SchoolApplication.Instance(this);
            AggregateRootId = Guid.NewGuid();
        }

        protected void GivenADraftCalendarHasBeenCreated()
        {
            var createCommand = OM.Commands.CreatePlanningCalendar();
            AggregateRootId = createCommand.Id;
            sut.Process(createCommand);
        }
        protected void PreviousTopicsWereAppended(int existingTopicCount)
        {
            while (existingTopicCount > 0)
            {
                var aCommand = OM.Commands.AppendTopicCommand(AggregateRootId, OM.Generator.Create("Title "), OM.Generator.Create("Description "));
                sut.Process(aCommand);
                existingTopicCount--;
            }
        }
        protected void PreviousEventsWereAppended(int existingEvaluationCount)
        {
            while (existingEvaluationCount > 0)
            {
                var aCommand = OM.Commands.AppendEvaluationCommand(AggregateRootId, OM.Generator.Create("Title "));
                sut.Process(aCommand);
                existingEvaluationCount--;
            }
        }
        protected void ThenTheExpectedExceptionIsGenerated<T>() where T : Exception
        {
            Assert.IsType<T>(ActualExeption);
        }
        protected void ExecuteActionThatThrows(Action action)
        {
            ActualExeption = TestHelpers.ExecuteActionThatThrows(action);
        }
    }
}

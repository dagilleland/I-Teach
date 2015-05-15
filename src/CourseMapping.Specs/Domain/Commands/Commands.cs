using CourseMapping.Commands;
using FakeItEasy;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace CourseMapping.Specs.Domain.Commands
{
    [Story(IWant="I want to propose new courses",
           SoThat="So that new courses can be planned for future release")]
    public class Proposing_New_Courses
    {
        [Fact]
        public void Scenario_Name()
        {
            ProposeCourse command = null;
            this.When(_ => WhenIProposeANewCourse("Domain Driven Design", "Program of Study", out command))
                .Then(_ => ThenICanUniquelyIdentifyTheCourse(command))
                //.And(_ => ThenTheCourseIsProposed
                .BDDfy();
        }
        private void WhenIProposeANewCourse(string courseName, string programOfStudy, out ProposeCourse command)
        {
            command = new ProposeCourse(courseName, programOfStudy);
        }
        private void ThenICanUniquelyIdentifyTheCourse(ProposeCourse command)
        {
            Assert.NotEqual(Guid.Empty, command.AggregateRootId);
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to add an existing course",
           SoThat = "So that courses currently available are included in the course mapping")]
    public class Adding_Existing_Courses
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to assign a course number to a proposed course",
           SoThat = "So that the course can be identified in the course mapping")]
    public class Assigning_Course_Numbers
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to assign a course name to a proposed/revised course",
           SoThat = "So that the course name can act as a description for the course.")]
    public class Assigning_Course_Names
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to adjust course details",
           SoThat = "So that proposed and revised courses can be modified")]
    public class Adjust_Course_Details
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to add course dependencies",
           SoThat = "So that course corequesites and prerequisites can be mapped")]
    public class Adding_Course_Dependencies
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to adjust the importance of course dependencies",
           SoThat = "So that the nature of course dependencies can be adjusted")]
    public class Adjusting_Course_Dependencies
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to remove course dependencies",
           SoThat = "So that coursess can be decoupled in the course mapping")]
    public class Removing_Course_Dependencies
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to accept proposed courses",
           SoThat = "So that they can be included in the course mapping")]
    public class Accepting_Proposed_Courses
    {
        [Fact]
        public void Scenario_Name()
        {
            throw new NotImplementedException();
        }
    }

    [Story(IWant = "I want to reject proposed courses",
           SoThat = "So that they can be removed from the course mapping")]
    public class Rejecting_Proposed_Courses
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to make changes to current courses",
           SoThat = "So that courses can be modified for future delivery")]
    public class Revising_Current_Courses
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to accept course revisions",
           SoThat = "So that they can be included in the course mapping")]
    public class Accepting_Course_Revisions
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to reject course revisions",
           SoThat = "So that they can be removed from the course mapping")]
    public class Rejecting_Course_Revisions
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }

    [Story(IWant = "I want to retire current courses",
           SoThat = "So that courses that are no longer applicable to the program will no longer be available")]
    public class Retiring_Current_Courses
    {
        [Fact]
        public void Scenario_Name()
        {
            this.When(_ => TBA()).BDDfy();
        }
        public void TBA()
        {
            { throw new NotImplementedException(); }
        }
    }
}

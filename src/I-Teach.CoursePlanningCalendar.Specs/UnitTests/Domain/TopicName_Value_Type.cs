using CommonUtilities.Exceptions;
using I_Teach.CoursePlanningCalendar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace I_Teach.CoursePlanningCalendar.Specs.UnitTests.Domain
{
    public class TopicName_Value_Type
    {
        [Fact]
        public void Should_Explicitly_Convert_From_String()
        {
            Name name = (Name)"C# Implicit Operators";
            Assert.Equal<string>("C# Implicit Operators", name);
        }
        [Fact]
        public void Should_Implicitly_Convert_To_String()
        {
            Name name = (Name)"C# Implicit Operators";
            Assert.Equal<string>("C# Implicit Operators", name);
        }

        [Fact]
        public void Should_Support_Equals()
        {
            Name name = (Name)"C# Implicit Operators";
            Name byAnotherName = (Name)"C# Implicit Operators";
            Assert.Equal<Name>(name, byAnotherName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("\t")]
        [InlineData(null)]
        public void Should_Reject_Whitespace_As_Title(string title)
        {
            Assert.Throws<NullOrWhiteSpaceStringException>(() => { Name name = (Name)title; });
        }
    }
}

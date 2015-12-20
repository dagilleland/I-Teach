using I_Teach.ClassMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace I_Teach.ClassMarks.Specs.UnitTests
{
    public class StudentId_Value_Type
    {
        [Fact]
        public void Should_Support_Equality_Comparisons()
        {
            Guid actual = Guid.NewGuid();
            var first = new StudentId(actual);
            var second = new StudentId(actual);

            Assert.Equal(first, second);
        }

        [Fact]
        public void Should_Reject_Empty_Guid()
        {
            Assert.Throws<DomainStateException>(() => new StudentId(Guid.Empty));
        }
    }
}

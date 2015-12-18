using I_Teach.ClassMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace I_Teach.ClassMarks.Specs.UnitTests
{
    public class Course_Entity_Model
    {
    }
    public class Student_Entity_Model
    {
        [Fact]
        public void Should_Reject_Empty_First_Name()
        {
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), null, "Dent"));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "", "Dent"));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "   ", "Dent"));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "\t", "Dent"));
        }

        [Fact]
        public void Should_Reject_Empty_Id()
        {
            Assert.Throws<DomainStateException>(() => new Student(null, "Stewart", "Dent"));
        }
    }
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

using I_Teach.ClassMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace I_Teach.ClassMarks.Specs.UnitTests
{
    public class Student_Entity_Model
    {
        [Fact]
        public void Should_Intantiate()
        {
            var expectedId = new StudentId(Guid.NewGuid());
            var sut = new Student(expectedId, "Stewart", "Dent");
            // TODO: Check public values
        }

        [Fact]
        public void Should_Reject_Empty_First_Name()
        {
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), null, "Dent"));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "", "Dent"));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "   ", "Dent"));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "\t", "Dent"));
        }

        [Fact]
        public void Should_Reject_Empty_Last_Name()
        {
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "Stewart", null));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "Stewart", ""));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "Stewart", "   "));
            Assert.Throws<DomainStateException>(() => new Student(new StudentId(Guid.NewGuid()), "Stewart", "\t"));
        }

        [Fact]
        public void Should_Reject_Null_Id()
        {
            Assert.Throws<DomainStateException>(() => new Student(null, "Stewart", "Dent"));
        }
    }
}

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
        [Fact]
        public void Should_Reject_Null_Section()
        {
            Assert.Throws<DomainStateException>(() => I_Teach.ClassMarks.Models.Class.CreateClass(null, null, null));
        }
        [Fact]
        public void Should_Reject_Null_Term()
        {
            Assert.Throws<DomainStateException>(() => I_Teach.ClassMarks.Models.Class.CreateClass(null, null, null));
        }
        [Fact]
        public void Should_Reject_Null_CourseEvaluations()
        {
            Assert.Throws<DomainStateException>(() => I_Teach.ClassMarks.Models.Class.CreateClass(null, null, null));
        }
        [Fact]
        public void Should_Reject_Empty_CourseEvaluations()
        {
            Assert.Throws<DomainStateException>(() => I_Teach.ClassMarks.Models.Class.CreateClass(null, null, null));
        }


    }
}

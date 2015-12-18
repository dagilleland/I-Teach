using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace I_Teach.ClassMarks.Models
{
    public class StudentId
    {
        public Guid Value { get; private set; }

        public StudentId(Guid guid)
        {
            if (guid.Equals(Guid.Empty)) throw new DomainStateException();
            Value = guid;
        }
    }
}

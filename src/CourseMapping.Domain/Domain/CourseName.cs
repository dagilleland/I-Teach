using System;
using CourseMapping.Domain.Exceptions;

namespace CourseMapping.Domain
{
    class CourseName
    {
        public string Name { get; private set; }
        public CourseName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new NullOrWhiteSpaceStringException("course name cannot be empty");
            Name = name;
        }
        public override string ToString() { return Name; }
    }
}

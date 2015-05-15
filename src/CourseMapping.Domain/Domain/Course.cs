using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseMapping.Domain
{
    class Course : SimpleCqrs.Domain.AggregateRoot
    {
        public CourseName Name { get; private set; }
        public CourseNumber Number { get; private set; }
        public int Hours { get; private set; }
        public double Credits { get; private set; }
        public Term CommencementDate { get; private set; }
        public Term FinalOfferingDate { get; private set; }
    }
}
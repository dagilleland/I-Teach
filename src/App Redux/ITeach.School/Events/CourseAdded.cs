using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.School.Events
{
    public class CourseAdded
    {
        public string CourseNumber { get; set; }
        public string CourseName { get; set; }
        public double Credits { get; set; }
        public int TotalHours { get; set; }
    }
    public class CourseScheduled
    {
        public string CourseNumber { get; set; }
        public string Term { get; set; }
        public int Year { get; set; }
        public string[] Sections { get; set; }
    }
}

using CommonUtilities.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Teach.CoursePlanningCalendar.Commands
{
    public class CreatePlanningCalendar : CommandWithAggregateRootId
    {
        public string CourseName { get; private set; }
        public string CourseNumber { get; private set; }
        public int TotalHours { get; set; }
        public int ClassesPerWeek { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePlanningCalendar"/> class.
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="courseNumber"></param>
        /// <param name="totalHours"></param>
        /// <param name="classesPerWeek"></param>
        public CreatePlanningCalendar(string courseName, string courseNumber, int totalHours, int classesPerWeek)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                throw new ArgumentException("courseName is null or empty.", "courseName");
            if (string.IsNullOrWhiteSpace(courseNumber))
                throw new ArgumentException("courseNumber is null or empty.", "courseNumber");
            if (totalHours != 60 && totalHours != 75 && totalHours != 90 && totalHours != 180)
                throw new ArgumentException("total course hours must be either 0, 75, 90, or 180", "totalHours");
            if (classesPerWeek != 2 && classesPerWeek != 3 && classesPerWeek != 4 && classesPerWeek != 5)
                throw new ArgumentException("classes per week must be either 2, 3, 4, or 5", "classesPerWeek");
            CourseName = courseName;
            CourseNumber = courseNumber;
            TotalHours = totalHours;
            ClassesPerWeek = classesPerWeek;
            Id = Guid.NewGuid();
        }
    }
}

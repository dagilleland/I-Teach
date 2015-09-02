using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace I_Teach.CoursePlanningCalendar.Fetch.Model
{
    public class DraftPlanningCalendar
    {
        [Key]
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string CourseNumber { get; set; }
        public int? Year { get; set; }
        public string Month { get; set; }
        public int TotalHours { get; set; }
        public int ClassesPerWeek { get; set; }
    }
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public Guid PlanningCalendarId { get; set; }
        public string Description { get; set; }
        public double Duration { get; set; }
        public int Sequence { get; set; }
    }
}

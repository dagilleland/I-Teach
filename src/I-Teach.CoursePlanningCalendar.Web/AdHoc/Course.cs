using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace I_Teach.CoursePlanningCalendar.Web.AdHoc
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Number { get; set; }
        public int TotalHours { get; set; }
        public int Weeks { get; set; }
    }
}

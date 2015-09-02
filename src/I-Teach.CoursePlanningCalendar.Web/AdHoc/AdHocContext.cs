using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace I_Teach.CoursePlanningCalendar.Web.AdHoc
{
    internal class AdHocContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdHocContext"/> class.
        /// </summary>
        public AdHocContext() : base("DefaultConnection") { }

        public DbSet<Course> Courses { get; set; }
    }

}
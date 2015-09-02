namespace I_Teach.CoursePlanningCalendar.Web.Migrations
{
    using I_Teach.CoursePlanningCalendar.Web.AdHoc;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<I_Teach.CoursePlanningCalendar.Web.AdHoc.AdHocContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(I_Teach.CoursePlanningCalendar.Web.AdHoc.AdHocContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Courses.AddOrUpdate(c => c.Id,
                new Course { Id = Guid.NewGuid(), Number = "PROG1005", Name = "Introduction to Programming", TotalHours = 90, Weeks = 15 },
                new Course { Id = Guid.NewGuid(), Number = "PROG1009", Name = "Database Fundamentals", TotalHours = 90, Weeks = 15 },
                new Course { Id = Guid.NewGuid(), Number = "PROG2045", Name = "Domain Driven Design", TotalHours = 90, Weeks = 15 },
                new Course { Id = Guid.NewGuid(), Number = "PROG2122", Name = "Implementing Domain Driven Design", TotalHours = 90, Weeks = 15 });
        }
    }
}

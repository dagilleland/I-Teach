using I_Teach.CoursePlanningCalendar.Events.Model;
using I_Teach.CoursePlanningCalendar.Fetch.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace I_Teach.CoursePlanningCalendar.Fetch
{
    internal class ReadModelDataStore : DbContext
    {
        public ReadModelDataStore(string connectionStringName) : base(connectionStringName) { }

        #region For the Event Store
        public DbSet<Aggregate> Aggregates { get; set; }
        public DbSet<Event> Events { get; set; }
        #endregion

        #region Read Model
        public DbSet<DraftPlanningCalendar> DraftPlanningCalendars { get; set; }
        public DbSet<Topic> Topics { get; set; }
        #endregion
    }
}

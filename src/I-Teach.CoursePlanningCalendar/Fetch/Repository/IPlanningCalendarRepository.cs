using I_Teach.CoursePlanningCalendar.Fetch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Teach.CoursePlanningCalendar.Fetch
{
    public interface IPlanningCalendarRepository
    {
        DraftPlanningCalendar FindDraftPlanningCalendar(Guid id);
        IEnumerable<DraftPlanningCalendar> ListDraftPlanningCalendars();
        IEnumerable<Topic> ListTopics(Guid draftPlanningCalendarId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Teach.ViewModels.CourseAdministrationDomain
{
    public class Course
    {
        public string Number { get; set; }
        public string Name { get; set; }
    }
    [Serializable]
    public class EvaluationItem
    {
        public string Name { get; set; }
        public int Weight { get; set; }
    }
    [Serializable]
    public class EvaluationGroup
    {
        public string Name { get; set; }
        public int? PassMark { get; set; }
        public List<EvaluationItem> Items { get; set; }

        public EvaluationGroup()
        {
            Items = new List<EvaluationItem>();
        }
    }

}

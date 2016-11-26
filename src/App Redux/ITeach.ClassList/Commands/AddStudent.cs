using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.ClassList.Commands
{
    public class AddStudent
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class OpenSectionForRegistration
    {
        public string CourseNumber { get; set; }
        public string Term { get; set; }
        public int Year { get; set; }
        public string Section { get; set; }
    }
}

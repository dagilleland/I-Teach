using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace I_Teach.ClassMarks.Models
{
    public class Student
    {
        private StudentId Id { get; private set; }
        private string FirstName { get; private set; }
        private string LastName { get; private set; }

        public Student(StudentId id, string firstName, string lastName)
        {
            // TODO: Complete member initialization
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}

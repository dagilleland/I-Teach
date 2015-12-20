using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace I_Teach.ClassMarks.Models
{
    public class Student
    {
        public StudentId Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Student(StudentId id, string firstName, string lastName)
        {
            // TODO: Complete member initialization
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}

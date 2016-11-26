using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
namespace ITeach.School.DomainModel
{
    internal class Course
    {
        public CourseNumber Number { get; set; }
        public CourseName Name { get; set; }
        public Hours Hours { get; set; }
        public Credits Credits { get; set; }

        public Course(CourseNumber number, CourseName name, Hours hours, Credits credits)
        {
            Contract.Requires(number != null, "number is null.");
            Contract.Requires(name != null, "name is null.");
            Contract.Requires(hours != null, "hours is null.");
            Contract.Requires(credits != null, "credits is null.");
            Number = number;
            Name = name;
            Hours = hours;
            Credits = credits;
        }
    }
    internal class CourseNumber
    {
        public string Value { get; private set; }

        public CourseNumber(string value)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is null or empty.");
            Value = value;
        }
    }
    internal class CourseName
    {
        public string Value { get; private set; }

        public CourseName(string value)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(value), "value is null or empty.");
            Value = value;
        }
    }
    internal class Credits
    {
        public double Value { get; private set; }

        public Credits(double value)
        {
            Contract.Requires(value > 0, "value is zero or negative");
            Value = value;
        }
    }
    internal class Hours
    {
        public int Value { get; private set; }

        public Hours(int value)
        {
            Contract.Requires(value > 0, "value is zero or negative");
            Value = value;
        }
    }

    internal class Term
    {
        public enum StartMonth { Sep, Jan, May }
        public StartMonth CalendarStart { get; private set; }
        public int Year { get; private set; }

        public Term(StartMonth semester, int year)
        {
            this.CalendarStart = semester;
            Year = year;
        }
    }

    internal class CourseOffering
    {
        public string OfferingId
        {
            get
            {
                return string.Format("{0}-{1}-{2}", CourseNumber.Value, Term.Year, Term.CalendarStart);
            }
        }
        public CourseNumber CourseNumber { get; set; }
        public Term Term { get; set; }

        public CourseOffering(CourseNumber courseNumber, Term term)
        {
            CourseNumber = courseNumber;
            Term = term;
        }
    }
}

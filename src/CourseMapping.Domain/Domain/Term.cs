using System;

namespace CourseMapping.Domain
{
    class Term
    {
        public Month Month { get; private set; }
        public int Year { get; private set; }

        public Term(Month month, int year)
        {
            if (year < 2010 || year > 2064) // I'll probably be gone by 2064, so this can be someone else's problem by then...
                throw new ArgumentException("invalid year");
            Month = month;
            Year = year;
        }

        public string SchoolYear
        {
            get
            {
                const string format = "{1}/{2}";
                string value = Month == Month.Sep ?
                    string.Format(format, Year, ((Year + 1) % 100).ToString("00"))
                    :
                    string.Format(format, Year - 1, (Year % 100).ToString("00"));
                return value;
            }
        }
    }
}

﻿using CommonInfrastructure;
using ITeach.School.Commands;
using ITeach.School.DomainModel;
using ITeach.School.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITeach.School.Aggregates
{
    public class AggregateRoot
        : IHandleCommand<AddCourse>
        , IHandleCommand<ScheduleCourse>
        , IApplyEvent<CourseAdded>
    {
        private Dictionary<string, Course> _Repository = new Dictionary<string, Course>();
        public IEnumerable<object> Handle(AddCourse command)
        {
            var course = CreateCourse(command.CourseNumber, command.CourseName, command.Credits, command.TotalHours);
            // Check rules

            _Repository.Add(command.CourseNumber, course);
            // Create event
            var result = new CourseAdded
            {
                CourseNumber = command.CourseNumber,
                CourseName = command.CourseName,
                Credits = command.Credits,
                TotalHours = command.TotalHours
            };

            return new object[] { result };
        }

        #region Factory Methods
        private Course CreateCourse(string courseNumber, string courseName, double credits, int hours)
        {
            var number = new CourseNumber(courseNumber);
            var name = new CourseName(courseName);
            var hoursa = new Hours(hours);
            var creditsa = new Credits(credits);
            return new Course(number, name, hoursa, creditsa);
        }
        #endregion

        public IEnumerable<object> Handle(ScheduleCourse command)
        {
            throw new NotImplementedException();
        }

        public void Apply(CourseAdded theEvent)
        {

        }
    }
}
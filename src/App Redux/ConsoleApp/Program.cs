using Autofac;
using CommonInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static IContainer Container { get; set; }
        public static void Main(string[] args)
        {
            // Register types with a builder
            var builder = new ContainerBuilder();
            //bulider.RegisterType<CommandModule>().As<Autofac.Core.IModule>();
            builder.RegisterType<ITeach.ApplicationHost.Publisher>().As<IPublishEvents>();
            builder.RegisterType<ITeach.ApplicationHost.SchoolApplication>().As<IProcessCommand>();
            builder.RegisterType<Program>();

            // Make the container
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<Program>();
                app.Run();
            }
        }

        private IProcessCommand SA { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        /// <param name="sA"></param>
        public Program(IProcessCommand sA)
        {
            SA = sA;
        }
        #region School Commands
        private void AddCourse(string courseNumber, string courseName, double credits, int totalHours)
        {
            // Add a course
            var addCommand = new ITeach.School.Commands.AddCourse()
            {
                CourseNumber = courseNumber,
                CourseName = courseName,
                Credits = credits,
                TotalHours = totalHours
            };
            SA.Process(addCommand); //AR_School.Handle(addCommand);
        }
        private void ScheduleCourse(string courseNumber, string termMonth, int year, string[] sections)
        {
            var scheduleCourse = new ITeach.School.Commands.ScheduleCourse
            {
                CourseNumber = courseNumber,
                Term = termMonth,
                Year = year,
                Sections = sections
            };
            SA.Process(scheduleCourse); // var results = AR_School.Handle();
        }
        #endregion

        #region CourseEvaluation Commands
        private void ProvisionCourseEvaluation(string courseNumber)
        {
            var provisionCourseEvaluation = new ITeach.CourseEvaluation.Commands.ProvisionCourseEvaluation()
            {
                CourseNumber = courseNumber
            };
            SA.Process(provisionCourseEvaluation); //AR_CourseEvaluation.Handle();
        }
        private void AddEvaluationGroup(string courseNumber, string groupName, int? passMark = null)
        {
            var addEvaluationGroup = new ITeach.CourseEvaluation.Commands.AddEvaluationGroup()
            {
                CourseNumber = courseNumber,
                Name = groupName,
                Pass = passMark
            };
            SA.Process(addEvaluationGroup); //AR_CourseEvaluation.Handle();
        }
        private void AddEvaluationGroupItem(string courseNumber, string groupName, string itemName, int weight)
        {
            var addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
                        {
                            CourseNumber = courseNumber,
                            GroupName = groupName,
                            Name = itemName,
                            Weight = weight
                        };
            SA.Process(addEvaluationItem); //AR_CourseEvaluation.Handle();
        }
        private void AddEvaluationItem(string courseNumber, string itemName, int weight)
        {
            var addSingleEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationItem()
            {
                CourseNumber = courseNumber,
                Name = itemName,
                Weight = weight
            };
            SA.Process(addSingleEvaluationItem); //AR_CourseEvaluation.Handle();
        }
        private void UseDraftEvaluationAsCurrent(string courseNumber)
        {
            var useDraftEvaluationAsCurrent = new ITeach.CourseEvaluation.Commands.UseDraftEvaluationAsCurrent()
            {
                CourseNumber = courseNumber
            };
            SA.Process(useDraftEvaluationAsCurrent); //AR_CourseEvaluation.Handle();
        }
        #endregion

        void Run()
        {
            //var AR_School = new ITeach.School.Aggregates.AggregateRoot();
            const string courseNumber = "COMP1001";

            AddCourse(courseNumber, "Introduction to Computer Programming", 4.5, 90);

            // Add its evaluation items
            //  Theory: 50%     Q1=10, Q2=15, Midterm=20, Final=20,
            //  Labs:   50%     Lab1=10, Lab2=15,
            //  Exercises=10
            //var AR_CourseEvaluation = new ITeach.CourseEvaluation.Aggregates.AggregateRoot();
            ProvisionCourseEvaluation(courseNumber);
            AddEvaluationGroup(courseNumber, "Theory", 50);
            AddEvaluationGroupItem(courseNumber, "Theory", "Q1", 10);
            AddEvaluationGroupItem(courseNumber, "Theory", "Q2", 15);
            AddEvaluationGroupItem(courseNumber, "Theory", "Midterm", 20);
            AddEvaluationGroupItem(courseNumber, "Theory", "Final", 20);

            AddEvaluationGroup(courseNumber, "Lab", 50);
            AddEvaluationGroupItem(courseNumber, "Lab", "Lab 1", 10);
            AddEvaluationGroupItem(courseNumber, "Lab", "Lab 2", 15);

            AddEvaluationItem(courseNumber, "Exercises", 10);

            UseDraftEvaluationAsCurrent(courseNumber);
            // TBA - Add an instructor - Annin Struckter

            // Schedule Course Sections (course number, term, section)
            ScheduleCourse(courseNumber, "Sep", 2016, new string[] { "S01", "S02", "S05" });

            // Saga
            // - copies over course evaluation
            var AR_ClassList = new ITeach.ClassList.Aggregates.AggregateRoot();
            var openCourse = new ITeach.ClassList.Commands.OpenSectionForRegistration()
            {

            };
            // - sets up an empty class list
            // Add List of Students
            // Add student (one-by-one, each atomic)
            var addStudent = new ITeach.ClassList.Commands.AddStudent
            {
                StudentId = "1",
                FirstName = "First",
                LastName = "Student"
            };

            addStudent = new ITeach.ClassList.Commands.AddStudent
            {
                StudentId = "2",
                FirstName = "Stew",
                LastName = "Dent"
            };

            addStudent = new ITeach.ClassList.Commands.AddStudent
            {
                StudentId = "3",
                FirstName = "Class",
                LastName = "Clown"
            };

            // Assign student some marks
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var AR_School = new ITeach.School.Aggregates.AggregateRoot();

            // Add a course
            var addCommand = new ITeach.School.Commands.AddCourse()
            {
                CourseNumber = "COMP1001",
                CourseName = "Introduction to Computer Programming",
                Credits = 4.5,
                TotalHours = 90
            };
            AR_School.Handle(addCommand);


            // Add its evaluation items
            //  Theory: 50%     Q1=10, Q2=15, Midterm=20, Final=20,
            //  Labs:   50%     Lab1=10, Lab2=15,
            //  Exercises=10
            var AR_CourseEvaluation = new ITeach.CourseEvaluation.Aggregates.AggregateRoot();
            var provisionCourseEvaluation = new ITeach.CourseEvaluation.Commands.ProvisionCourseEvaluation
            {
                CourseNumber = "COMP1001"
            };
            AR_CourseEvaluation.Handle(provisionCourseEvaluation);

            var addEvaluationGroup = new ITeach.CourseEvaluation.Commands.AddEvaluationGroup
            {
                CourseNumber = "COMP1001",
                Name = "Theory",
                Pass = (int?) 50
            };
            AR_CourseEvaluation.Handle(addEvaluationGroup);
            var addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
            {
                CourseNumber = "COMP1001",
                GroupName = "Theory",
                Name = "Q1",
                Weight = 10
            };
            AR_CourseEvaluation.Handle(addEvaluationItem);
            addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
            {
                CourseNumber = "COMP1001",
                GroupName = "Theory",
                Name = "Q2",
                Weight = 15
            };
            AR_CourseEvaluation.Handle(addEvaluationItem);
            addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
            {
                CourseNumber = "COMP1001",
                GroupName = "Theory",
                Name = "Midterm",
                Weight = 20
            };
            AR_CourseEvaluation.Handle(addEvaluationItem);
            addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
            {
                CourseNumber = "COMP1001",
                GroupName = "Theory",
                Name = "Final",
                Weight = 20
            };
            AR_CourseEvaluation.Handle(addEvaluationItem);

            addEvaluationGroup = new ITeach.CourseEvaluation.Commands.AddEvaluationGroup
            {
                CourseNumber = "COMP1001",
                Name = "Lab",
                Pass = (int?) 50
            };
            AR_CourseEvaluation.Handle(addEvaluationGroup);

            addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
            {
                CourseNumber = "COMP1001",
                GroupName = "Lab",
                Name = "Lab 1",
                Weight = 10
            };
            AR_CourseEvaluation.Handle(addEvaluationItem);
            addEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationGroupItem
            {
                CourseNumber = "COMP1001",
                GroupName = "Lab",
                Name = "Lab 2",
                Weight = 15
            };
            AR_CourseEvaluation.Handle(addEvaluationItem);
            var addSingleEvaluationItem = new ITeach.CourseEvaluation.Commands.AddEvaluationItem
            {
                CourseNumber = "COMP1001",
                Name = "Exercises",
                Weight = 10
            };
            AR_CourseEvaluation.Handle(addSingleEvaluationItem);

            var useDraftEvaluationAsCurrent = new ITeach.CourseEvaluation.Commands.UseDraftEvaluationAsCurrent
            {
                CourseNumber = "COMP1001"
            };
            AR_CourseEvaluation.Handle(useDraftEvaluationAsCurrent);

            // TBA - Add an instructor - Annin S T Ruckter

            // Schedule Course Sections (course number, term, section)
            var scheduleCourse = new ITeach.School.Commands.ScheduleCourse
            {
                CourseNumber = "COMP1001",
                Term = "Sep",
                Year = 2016,
                Sections = new string[] {"S01", "S02", "S05"}
            };
            AR_School.Handle(scheduleCourse);

                // Saga
                    // - copies over course evaluation
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

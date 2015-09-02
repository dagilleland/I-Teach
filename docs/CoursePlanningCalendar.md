# Course Planning Calendar

In DMIT, we plan how we are going to deliver our courses and course offerings on a regular basis. Every term, a Planning Calendar is created for each course to organize the topics that will be covered and the exam/lab dates. During Intercession, the overall topic flow is planned and settled on, along with the week in which exams and labs will be released and due. Then, at the very start of the term, specific dates for exams and labs will be chosen based on the various schedules for the sections of the course offering.

## Simplest Possible Thing

As a *course coordinator*, I want a way to create and edit a Planning Calendar to organize the weekly topics for a course along with their evaluation components (buckets, labs, exams, etc.). I want to be able to share this planning calendar with other instructors for the course and then publish it for the students to see at the start of the term. Instructors may need to make adjustments to their individual copies of the planning calendar one or more times as the course progresses, for various reasons. In general, all the instructors try to start with the same planning calendar, but they may diverge slightly as the course progresses.

Each course is 15 weeks long. Planning calendars can be copied/applied to various terms (e.g.: Fall 2014 or Winter 2015) and sections.

Topics on the Planning Calendar are short descriptions of what is being covered at a particular point in the course. Students only need to see those short topic names, but as Instructors we would benefit from including additional details on the topics. Those details might consist of things such as a list of key points or objectives, comments about the topic, notes about the in-class examples to be used or the scenarios to be used for testing (labs/exams), etc. This extra information can be helpful in planning the **sequence** of the topics (and in-class evaluations) and also help the instructor in remembering important aspects of the topic when it comes time to teach it in class.

### Courses and Course Offerings

**Courses** and **Course Offerings** form the central backdrop to developing a Planning Calendar. Key information about courses and course offerings include the following:

- **Course** information includes
  - A *Name* and *Number* that uniquely identifies the course.
  - The total number of *Hours* and *Credits* for the course
  - A general classification of the type of course as either a *Classroom Course*, a *Lab Component Course* (the default), an *Independent Study course*, or a *Capstone* course
  - A *Date* for when course offerings can commence and, when a course is being sunsetted, the *Final Offering Date* after which the course will no longer be offered.
- **Course Offering** information expands on the general course information by describing a particular term in which the course is available for students to take. It includes
  - The *Term* and *Year* in which the course is offered (such as "Jan 2016" or "Sep 2017")

Ultimately, a Course Offering will be made available as one or more *Sections* that students can enroll in. An *Instructor* will be assigned to each *Section*, and as the course progresses, the Instructor(s) may need to make adjustments to their planning calendars to accommodate changing events or the needs of their particular section. By the time the instructors are needing the Course Calendars for their sections, the specific *Starting Date* of the course offering should be known along with the specific days of the week that classes will occur (classes are typically done in one and two hour blocks for Classroom and Lab Component courses).

----

## User Stories

### Draft Planning Calendars

- **Create Draft Calendar**
  - *In order to* **have a working copy of a planning calendar for editing**
  - *As a(n)* **Course Coordinator**
  - *I want to* **create a draft planning calendar for a course**
    - **Create a blank draft**
    - **Create a draft from an existing calendar**

- **Record Non-School Days**
  - *In order to* **ensure there is enough time for all the items on the planning calendar**
  - *As a(n)* **Course Coordinator**
  - *I want to* **edit the dates in the upcoming term that will be missed due to holidays or skipped due to reading breaks**
    - **Edit Holiday Dates**
    - **Add Reading Week**

- **Edit Topics**
  - *In order to* **plan the topics to cover in class**
  - *As a(n)* **Course Coordinator**
  - *I want to* **edit the list of topics**
    - **Add a topic**
    - **Change a topic**
    - **Remove a topic**
    - **Reorder topics**

- **Include Work Periods**
  - *In order to* **allow time for in-class work**
  - *As a(n)* **Course Coordinator**
  - *I want to* **add or remove work periods between topics**
    - **Add Work Period**
    - **Remove Work Period**

- **Edit Evaluations**
  - *In order to* **prepare students for evaluations**
  - *As a(n)* **Course Coordinator**
  - *I want to* **assign dates to evaluation components**
    - **Add an evaluation component**
    - **Change an evaluation component**
    - **Remove an evaluation component**

- **Accept Draft Calendar**
  - *In order to* **give instructors a planning calendar to use in their classes**
  - *As a(n)* **Course Coordinator**
  - *I want to* **release the draft course planning calendar for a specific term to the instructors**
    - **Accept Draft Calendar for Release**

### Section Specific Planning Calendars

- **Customize Class Days For Sections**
  - *In order to* **make planning calendars specific to each section**
  - *As a(n)* **Instructor**
  - *I want to* **enter the days of the week that my section has classes**
    - **Enter Class Days For Section**

- **Publish Course Calendar**
  - *In order to* **share the calendar with students**
  - *As a(n)* **Instructor**
  - *I want to* **publish my course planning calendar**
    - **Publish Calendar**

- **Adjust Topics**
  - *In order to* **adjust the course delivery for my section**
  - *As a(n)* **Instructor**
  - *I want to* **change the order and/or duration of topics on the calendar**
    - **Reorder Topics**
    - **Change Topic Duration**
    - **Add Work Period**
    - **Remove Work Period**

- **Reschedule Evaluation Dates**
  - *In order to* **provide proper preparation for evaluations in response to changes in the order of topics for the course**
  - *As a(n)* **Instructor**
  - *I want to* **adjust due dates for an evaluation item**
    - **Reschedule Evaluation Date**

- **Remove Topic**
  - *In order to* **compensate for insufficient time to cover topics in the course**
  - *As a(n)* **Instructor**
  - *I want to* **flag topics as "not covered"**
    - **Flag Topic As Not Covered**
      - *should include a comment as to why*

- **Remove Evaluation Component**
  - *In order to* **compensate for insufficient time to cover topics in the course**
  - *As a(n)* **Instructor**
  - *I want to* **flag evaluation components as "removed"**
      - ***must** include a note about how marks will be **redistributed***
    - **Flag Evaluation Component As Redistributed**
      - *should include a comment as to why*


----

# Sandbox Notes

Things to test:

- UI – manual testing (ignore automated testing for now)
  - Enter/edit topics/evaluation components for each week
  - Reorder topics/evaluations
  - Bulk-save the changes to the planning calendar
- Domain –
  - A course will have at most one current planning calendar and zero or more previous calendars. A planning calendar lists a week-by-week or class-by-class plan of topics along with any evaluation components (labs, exams, etc.) that will occur over the term. It is not unusual for topics to span more than one class or week; that is, a topic can have an estimated duration (in terms of days or class periods), though it is not required for placing the topic on the calendar.
  - An active planning calendar is one that is being used for one or more sections of a specific course offering by an instructor. Instructors are allowed to make modifications to the planning calendars for their sections, but should do so in consultation with other instructors, particularly in regard to changing the dates of any evaluation components.
  - Events
    - Add Course
    - Create Planning Calendar
    - Modify Planning Calendar
      - Add Topic
      - Reorder Topics
    - Apply Planning Calendar to Course Offering
    - Publish Planning Calendar
    - Assign Dates to Evaluation Components
    - Modify Active Planning Calendar
    - Link Topic to Resources (notes, readings, etc.)

----

## User Stories

- **Title**
  - *In order to* **BENEFIT**
  - *As a(n)* **ROLE**
  - *I want to* **COMMAND**
    - **SCENARIO**
    - **SCENARIO**
    - **SCENARIO**

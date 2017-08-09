using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingLogging.Services
{
    public class CourseService
    {
        private readonly ILogger<CourseService> _logger;

        public List<Course> Courses { get; } = new List<Course>();

        public CourseService(ILogger<CourseService> logger)
        {
            _logger = logger;
        }
        public void Enroll(Course course, Student student)
        {
            course.EnrolledStudents.Add(student);
        }
    }

    public class Course
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; set; }
        public int Capacity { get; set; } = 5;

        public List<Student> EnrolledStudents { get; } = new List<Student>();
    }

    public class Student
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}

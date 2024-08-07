using Moq;
using SIMS.Abstractions;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace SIMS_UnitTest
{
    public class CourseServiceTests
    {
        private readonly Mock<ICourseService> _mockCourseService;
        private readonly CourseContextCSV _courseContextCSV;

        public CourseServiceTests()
        {
            _mockCourseService = new Mock<ICourseService>();
            _courseContextCSV = new CourseContextCSV(_mockCourseService.Object);
        }

        [Fact]
        public void GetCourses_ReturnsCourseList()
        {
            // Arrange
            var expectedCourses = new List<Course>
        {
            new Course { CourseCode = "C001", CourseName = "Math 101", StartDate = new DateTime(2024, 1, 10), EndDate = new DateTime(2024, 5, 20), Description = "Basic Mathematics" },
            new Course { CourseCode = "C002", CourseName = "Physics 101", StartDate = new DateTime(2024, 2, 15), EndDate = new DateTime(2024, 6, 30), Description = "Introduction to Physics" }
        };

            _mockCourseService.Setup(service => service.GetCourses()).Returns(expectedCourses);

            // Act
            var actualCourses = _courseContextCSV.GetCourses().ToList();

            // Assert
            Assert.Equal(expectedCourses.Count, actualCourses.Count);
            Assert.Equal(expectedCourses.First().CourseCode, actualCourses.First().CourseCode);
        }

        [Fact]
        public void AddCourse_CallsAddCourseMethod()
        {
            // Arrange
            var course = new Course { CourseCode = "C003", CourseName = "Chemistry 101", StartDate = new DateTime(2024, 3, 1), EndDate = new DateTime(2024, 7, 10), Description = "Introduction to Chemistry" };

            // Act
            _courseContextCSV.AddCourse(course);

            // Assert
            _mockCourseService.Verify(service => service.AddCourse(It.Is<Course>(c => c.CourseCode == course.CourseCode)), Times.Once);
        }

        [Fact]
        public void UpdateCourse_CallsUpdateCourseMethod()
        {
            // Arrange
            var course = new Course { CourseCode = "C004", CourseName = "Biology 101", StartDate = new DateTime(2024, 4, 5), EndDate = new DateTime(2024, 8, 15), Description = "Introduction to Biology" };

            // Act
            _courseContextCSV.UpdateCourse(course);

            // Assert
            _mockCourseService.Verify(service => service.UpdateCourse(It.Is<Course>(c => c.CourseCode == course.CourseCode)), Times.Once);
        }

        [Fact]
        public void DeleteCourse_CallsDeleteCourseMethod()
        {
            // Arrange
            var courseCode = "C005";

            // Act
            _courseContextCSV.DeleteCourse(courseCode);

            // Assert
            _mockCourseService.Verify(service => service.DeleteCourse(It.Is<string>(c => c == courseCode)), Times.Once);
        }

        [Fact]
        public void GetCourse_ReturnsCorrectCourse()
        {
            // Arrange
            var courseCode = "C006";
            var expectedCourse = new Course { CourseCode = courseCode, CourseName = "History 101", StartDate = new DateTime(2024, 5, 10), EndDate = new DateTime(2024, 9, 20), Description = "Introduction to History" };

            _mockCourseService.Setup(service => service.GetCourse(courseCode)).Returns(expectedCourse);

            // Act
            var actualCourse = _courseContextCSV.GetCourse(courseCode);

            // Assert
            Assert.Equal(expectedCourse.CourseCode, actualCourse.CourseCode);
            Assert.Equal(expectedCourse.CourseName, actualCourse.CourseName);
        }
    }
}
    

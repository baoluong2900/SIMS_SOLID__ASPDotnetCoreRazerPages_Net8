using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS.Abstractions;
using SIMS.Model;
using SIMS.Services;
using Moq;
using Xunit;

namespace SIMS_Test
{   
    public class StudentServiceTests
    {
        private readonly Mock<IStudentService> _mockStudentContext;
        private readonly Mock<ICSVReader> _mockCSVReader;
        private readonly StudentService _studentService;

        public StudentServiceTests()
        {
            _mockStudentContext = new Mock<IStudentService>();
        }

        [Fact]
        public void GetStudents_ShouldReturnStudents_WhenStudentsExist()
        {
            // Arrange
            var students = new List<Student>
        {
            new Student { StudentID = 1, FirstName = "John Doe" },
            new Student { StudentID = 2, FirstName = "Jane Smith" }
        };
            _mockStudentContext.Setup(context => context.GetStudents()).Returns(students);

            // Act
            var result = _studentService.GetStudents();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].FirstName);
            Assert.Equal("Jane Smith", result[1].FirstName);
        }
    }
}

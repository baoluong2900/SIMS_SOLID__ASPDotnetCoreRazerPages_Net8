using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS.Abstractions;
using SIMS.Services;
using SIMS.Model;
using Moq;
using Xunit;
using OfficeOpenXml;
using CsvHelper;

namespace SIMS_UnitTest
{
    public class StudentServiceTests
    {
        private readonly Mock<IStudentService> _mockStudentService;
        private readonly StudentContextCSV _studentContextCSV;
        public StudentServiceTests()
        {
            _mockStudentService = new Mock<IStudentService>();
            _studentContextCSV = new StudentContextCSV(_mockStudentService.Object);
        }

        [Fact]
        public void GetStudents_ReturnsStudentList()
        {
            // Arrange
            var expectedStudents = new List<Student>
        {
            new Student { StudentNo = "S001", LastName = "Nguyen", FirstName = "Van A", UrlHandle = "handle1", Email = "a@example.com", DateOfBirth = new DateTime(2000, 1, 1) },
            new Student { StudentNo = "S002", LastName = "Tran", FirstName = "Thi B", UrlHandle = "handle2", Email = "b@example.com", DateOfBirth = new DateTime(1999, 5, 15) }
        };

            _mockStudentService.Setup(service => service.GetStudents()).Returns(expectedStudents);

            // Act
            var actualStudents = _studentContextCSV.GetStudents().ToList();

            // Assert
            Assert.Equal(expectedStudents.Count, actualStudents.Count);
            Assert.Equal(expectedStudents.First().StudentNo, actualStudents.First().StudentNo);
        }

        [Fact]
        public void AddStudent_CallsAddStudentMethod()
        {
            // Arrange
            var student = new Student { StudentNo = "S003", LastName = "Le", FirstName = "Thi C", UrlHandle = "handle3", Email = "c@example.com", DateOfBirth = new DateTime(2001, 3, 20) };

            // Act
            _studentContextCSV.AddStudent(student);

            // Assert
            _mockStudentService.Verify(service => service.AddStudent(It.Is<Student>(s => s.StudentNo == student.StudentNo)), Times.Once);
        }

        [Fact]
        public void UpdateStudent_CallsUpdateStudentMethod()
        {
            // Arrange
            var student = new Student { StudentNo = "S004", LastName = "Ho", FirstName = "Thi D", UrlHandle = "handle4", Email = "d@example.com", DateOfBirth = new DateTime(2002, 7, 10) };

            // Act
            _studentContextCSV.UpdateStudent(student);

            // Assert
            _mockStudentService.Verify(service => service.UpdateStudent(It.Is<Student>(s => s.StudentNo == student.StudentNo)), Times.Once);
        }

        [Fact]
        public void DeleteStudent_CallsDeleteStudentMethod()
        {
            // Arrange
            var studentNo = "S005";

            // Act
            _studentContextCSV.DeleteStudent(studentNo);

            // Assert
            _mockStudentService.Verify(service => service.DeleteStudent(It.Is<string>(s => s == studentNo)), Times.Once);
        }

        [Fact]
        public void GetStudent_ReturnsCorrectStudent()
        {
            // Arrange
            var studentNo = "S006";
            var expectedStudent = new Student { StudentNo = studentNo, LastName = "Vu", FirstName = "Thi E", UrlHandle = "handle5", Email = "e@example.com", DateOfBirth = new DateTime(2003, 9, 25) };

            _mockStudentService.Setup(service => service.GetStudent(studentNo)).Returns(expectedStudent);

            // Act
            var actualStudent = _studentContextCSV.GetStudent(studentNo);

            // Assert
            Assert.Equal(expectedStudent.StudentNo, actualStudent.StudentNo);
            Assert.Equal(expectedStudent.Email, actualStudent.Email);
        }
    }
}

using Moq;
using SIMS.Abstractions;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_UnitTest
{
    public class GradeServiceTests
    {
        private readonly Mock<IGradeService> _mockGradeService;
        private readonly GradeContextCSV _gradeContextCSV;

        public GradeServiceTests()
        {
            _mockGradeService = new Mock<IGradeService>();
            _gradeContextCSV = new GradeContextCSV(_mockGradeService.Object);
        }

        [Fact]
        public void GetGrades_ReturnsGradeList()
        {
            // Arrange
            var expectedGrades = new List<Grade>
        {
            new Grade { GradeId = "G001", StudentNo = "S001", CourseCode = "C001", Score = 85.5 },
            new Grade { GradeId = "G002", StudentNo = "S002", CourseCode = "C002", Score = 90.0 }
        };

            _mockGradeService.Setup(service => service.GetGrades()).Returns(expectedGrades);

            // Act
            var actualGrades = _gradeContextCSV.GetGrades().ToList();

            // Assert
            Assert.Equal(expectedGrades.Count, actualGrades.Count);
            Assert.Equal(expectedGrades.First().GradeId, actualGrades.First().GradeId);
        }

        [Fact]
        public void AddGrade_CallsAddGradeMethod()
        {
            // Arrange
            var grade = new Grade { GradeId = "G003", StudentNo = "S003", CourseCode = "C003", Score = 92.0 };

            // Act
            _gradeContextCSV.AddGrade(grade);

            // Assert
            _mockGradeService.Verify(service => service.AddGrade(It.Is<Grade>(g => g.GradeId == grade.GradeId)), Times.Once);
        }

        [Fact]
        public void UpdateGrade_CallsUpdateGradeMethod()
        {
            // Arrange
            var grade = new Grade { GradeId = "G004", StudentNo = "S004", CourseCode = "C004", Score = 88.0 };

            // Act
            _gradeContextCSV.UpdateGrade(grade);

            // Assert
            _mockGradeService.Verify(service => service.UpdateGrade(It.Is<Grade>(g => g.GradeId == grade.GradeId)), Times.Once);
        }

        [Fact]
        public void DeleteGrade_CallsDeleteGradeMethod()
        {
            // Arrange
            var gradeId = "G005";

            // Act
            _gradeContextCSV.DeleteGrade(gradeId);

            // Assert
            _mockGradeService.Verify(service => service.DeleteGrade(It.Is<string>(id => id == gradeId)), Times.Once);
        }

        [Fact]
        public void GetGrade_ReturnsCorrectGrade()
        {
            // Arrange
            var gradeId = "G006";
            var expectedGrade = new Grade { GradeId = gradeId, StudentNo = "S005", CourseCode = "C005", Score = 91.0 };

            _mockGradeService.Setup(service => service.GetGrade(gradeId)).Returns(expectedGrade);

            // Act
            var actualGrade = _gradeContextCSV.GetGrade(gradeId);

            // Assert
            Assert.Equal(expectedGrade.GradeId, actualGrade.GradeId);
            Assert.Equal(expectedGrade.Score, actualGrade.Score);
        }

        [Fact]
        public void GetGradesByStudentId_ReturnsGradesForStudent()
        {
            // Arrange
            var studentId = "S001";
            var expectedGrades = new List<Grade>
        {
            new Grade { GradeId = "G007", StudentNo = studentId, CourseCode = "C001", Score = 85.5 },
            new Grade { GradeId = "G008", StudentNo = studentId, CourseCode = "C002", Score = 90.0 }
        };

            _mockGradeService.Setup(service => service.GetGradesByStudentId(studentId)).Returns(expectedGrades);

            // Act
            var actualGrades = _gradeContextCSV.GetGradesByStudentId(studentId).ToList();

            // Assert
            Assert.Equal(expectedGrades.Count, actualGrades.Count);
            Assert.Equal(expectedGrades.First().GradeId, actualGrades.First().GradeId);
        }

        [Fact]
        public void GetGrades_ReturnsGradeListTest()
        {
            // Arrange
            var expectedGrades = new List<Grade>
        {
            new Grade { GradeId = "G001", StudentNo = "S001", CourseCode = "C001", Score = 85 },
            new Grade { GradeId = "G002", StudentNo = "S002", CourseCode = "C002", Score = 90 }
        };

            _mockGradeService.Setup(service => service.GetGrades()).Returns(expectedGrades);

            // Act
            var actualGrades = _gradeContextCSV.GetGrades().ToList();

            // Assert
            Assert.Equal(expectedGrades.Count, actualGrades.Count);
            Assert.Equal(expectedGrades.First().GradeId, actualGrades.First().GradeId);
        }
    }
}

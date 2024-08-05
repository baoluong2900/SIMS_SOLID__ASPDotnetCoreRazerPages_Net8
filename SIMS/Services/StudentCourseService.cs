using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using System.Globalization;

namespace SIMS.Services
{
    public class StudentCourseService: IStudentCourseService
    {
        private readonly ICSVReader _csvReader;
        private readonly string _filePath = "DataCSV/StudentCourseCSV.csv";
        private List<StudentCourse> _studentCourses;

        public StudentCourseService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
            _studentCourses = _csvReader.ReadCSV<StudentCourse>(_filePath).ToList();
        }
        public IEnumerable<StudentCourse> GetStudentCourses()
        {
            return _studentCourses;
        }
        public void AddStudentCourse(StudentCourse studentCourse)
        {
            _studentCourses.Add(studentCourse);
            SaveChanges();
        }

        public void UpdateStudentCourse(StudentCourse studentCourse)
        {
            var existingStudentCourse = _studentCourses.FirstOrDefault(s => s.StudentCourseNo == studentCourse.StudentCourseNo);
            if (existingStudentCourse != null)
            {
                existingStudentCourse.FirstName = studentCourse.FirstName;
                existingStudentCourse.LastName = studentCourse.LastName;
                existingStudentCourse.UrlHandle = studentCourse.UrlHandle;
                existingStudentCourse.Email = studentCourse.Email;
                existingStudentCourse.DateOfBirth = studentCourse.DateOfBirth;
                existingStudentCourse.CourseCode = studentCourse.CourseCode;
                SaveChanges();
            }
        }

        public void DeleteStudentCourse(string studentCourseNo)
        {
            var student = _studentCourses.FirstOrDefault(s => s.StudentCourseNo == studentCourseNo);
            if (student != null)
            {
                _studentCourses.Remove(student);
                SaveChanges();
            }
        }

        public StudentCourse GetStudentCourse(string studentCourseNo)
        {
            return _studentCourses.FirstOrDefault(s => s.StudentCourseNo == studentCourseNo);
        }

        private void SaveChanges()
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(_studentCourses);
        }
    }
}

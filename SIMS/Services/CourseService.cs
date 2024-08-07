using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using System.Formats.Asn1;
using System.Globalization;

namespace SIMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICSVReader _csvReader;
        private readonly string _filePath = "DataCSV/CourseCSV.csv";
        private List<Course> _courses;

        public CourseService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
            _courses = _csvReader.ReadCSV<Course>(_filePath).ToList();
        }

        public IEnumerable<Course> GetStudents()
        {
            return _courses;
        }

        private void SaveChanges()
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(_courses);
        }

        public IEnumerable<Course> GetCourses()
        {
            return _courses;
        }

        public void AddCourse(Course course)
        {
            _courses.Add(course);
            SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            var existingCourse = _courses.FirstOrDefault(s => s.CourseCode == course.CourseCode);
            if (existingCourse != null)
            {
                existingCourse.CourseName = course.CourseName;
                // cập nhạt thêm
                existingCourse.Description = course.Description;
                
                SaveChanges();
            }
        }

        public void DeleteCourse(string courseId)
        {
            var course = _courses.FirstOrDefault(s => s.CourseCode == courseId);
            if (course != null)
            {
                _courses.Remove(course);
                SaveChanges();
            }
        }

        public Course GetCourse(string courseId)
        {
            throw new NotImplementedException();
        }
    }
}

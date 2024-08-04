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

        public void AddStudent(Course student)
        {
            _courses.Add(student);
            SaveChanges();
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
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(string courseId)
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(string courseId)
        {
            throw new NotImplementedException();
        }
    }
}

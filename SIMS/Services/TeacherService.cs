using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using System.Globalization;

namespace SIMS.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ICSVReader _csvReader;
        private readonly string _filePath = "DataCSV/TeacherCSV.csv";
        private List<Teacher> _teachers;

        public TeacherService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
            _teachers = _csvReader.ReadCSV<Teacher>(_filePath).ToList();
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return _teachers;
        }

        public void AddTeacher(Teacher teacher)
        {
            _teachers.Add(teacher);
            SaveChanges();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var existingTeacher = _teachers.FirstOrDefault(t => t.TeacherNo == teacher.TeacherNo);
            if (existingTeacher != null)
            {
                existingTeacher.FirstName = teacher.FirstName;
                existingTeacher.LastName = teacher.LastName;
                existingTeacher.Email = teacher.Email;
                existingTeacher.DateOfBirth = teacher.DateOfBirth;
                existingTeacher.Department = teacher.Department;
                SaveChanges();
            }
        }

        public void DeleteTeacher(string teacherNo)
        {
            var teacher = _teachers.FirstOrDefault(t => t.TeacherNo == teacherNo);
            if (teacher != null)
            {
                _teachers.Remove(teacher);
                SaveChanges();
            }
        }

        public Teacher GetTeacher(string teacherNo)
        {
            return _teachers.FirstOrDefault(t => t.TeacherNo == teacherNo);
        }

        private void SaveChanges()
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(_teachers);
        }
    }

}

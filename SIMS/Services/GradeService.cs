using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using System.Globalization;

namespace SIMS.Services
{
    public class GradeService: IGradeService
    {
        private readonly ICSVReader _csvReader;
        private readonly string _filePath = "DataCSV/GradeCSV.csv";
        private List<Grade> _grades;

        public GradeService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
            _grades = _csvReader.ReadCSV<Grade>(_filePath).ToList();
        }

        public IEnumerable<Grade> GetGrades()
        {
            return _grades;
        }

        public void AddGrade(Grade grade)
        {
            _grades.Add(grade);
            SaveChanges();
        }

        public void UpdateGrade(Grade grade)
        {
            var existingGrade = _grades.FirstOrDefault(g => g.GradeId == grade.GradeId);
            if (existingGrade != null)
            {
                existingGrade.StudentNo = grade.StudentNo;
                existingGrade.CourseCode = grade.CourseCode;
                existingGrade.Score = grade.Score;
                SaveChanges();
            }
        }

        public void DeleteGrade(string gradeId)
        {
            var grade = _grades.FirstOrDefault(g => g.GradeId == gradeId);
            if (grade != null)
            {
                _grades.Remove(grade);
                SaveChanges();
            }
        }

        public Grade GetGrade(string gradeId)
        {
            return _grades.FirstOrDefault(g => g.GradeId == gradeId);
        }

        private void SaveChanges()
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(_grades);
        }
    }
}

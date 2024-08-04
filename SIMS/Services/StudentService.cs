﻿using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using System.Formats.Asn1;
using System.Globalization;

namespace SIMS.Services
{
    public class StudentService: IStudentService
    {
        private readonly ICSVReader _csvReader;
        private readonly string _filePath = "DataCSV/StudentCSV.csv";
        private List<Student> _students;

        public StudentService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
            _students = _csvReader.ReadCSV<Student>(_filePath).ToList();
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
            SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudentID == student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                SaveChanges();
            }
        }

        public void DeleteStudent(int studentId)
        {
            var student = _students.FirstOrDefault(s => s.StudentID == studentId);
            if (student != null)
            {
                _students.Remove(student);
                SaveChanges();
            }
        }

        public Student GetStudent(int studentId)
        {
            return _students.FirstOrDefault(s => s.StudentID == studentId);
        }

        private void SaveChanges()
        {
            using var writer = new StreamWriter(_filePath);
            using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(_students);
        }
    }
}
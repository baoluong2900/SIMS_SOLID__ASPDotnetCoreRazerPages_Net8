using SIMS.Abstractions;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.IO;
using CsvHelper;
public class StudentContextCSV : IStudentService
{
    private int nextStudentNo = 1;
    private readonly IStudentService _studentContext;
    public List<Student> Students { get; set; }
    private readonly string filePath = "DataCSV/StudentCSV.csv";

    private readonly ICSVReader _csvReader;

    public StudentContextCSV(IStudentService context)
    {
        _studentContext = context;
        Students = _csvReader.ReadCSV<Student>(filePath).ToList();
    }

    public IEnumerable<Student> GetStudents()
    {
        return Students;
    }

    public void AddStudent(Student student)
    {
        student.StudentNo = nextStudentNo++.ToString();
        Students.Add(student);
        WriteDataToCsv();
    }

    public void UpdateStudent(Student student)
    {
        var existingStudent = Students.FirstOrDefault(s => s.StudentNo == student.StudentNo);
        if (existingStudent != null)
        {
            existingStudent.LastName = student.LastName;
            existingStudent.FirstName = student.FirstName;
            existingStudent.UrlHandle = student.UrlHandle;
            existingStudent.Email = student.Email;
            existingStudent.DateOfBirth = student.DateOfBirth;
            WriteDataToCsv();
        }
    }

    public void DeleteStudent(string studentNo)
    {
        var student = Students.FirstOrDefault(s => s.StudentNo == studentNo);
        if (student != null)
        {
            Students.Remove(student);
            WriteDataToCsv();
        }
    }

    public Student GetStudent(string studentNo)
    {
        return Students.FirstOrDefault(s => s.StudentNo == studentNo);
    }

    private void WriteDataToCsv()
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
        csv.WriteRecords(Students);
    }
}

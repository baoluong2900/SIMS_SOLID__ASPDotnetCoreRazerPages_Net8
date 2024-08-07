using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using SIMS.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class GradeContextCSV : IGradeService
{
    private readonly IGradeService _gradeContext;
    private readonly ICSVReader _csvReader;
    private int nextGradeId = 1;
    public List<Grade> Grades { get; set; }
    private readonly string filePath = "DataCSV/GradeCSV.csv";

    // Constructor with default value for csvReader
    public GradeContextCSV(IGradeService gradeContext, ICSVReader? csvReader = null)
    {
        _gradeContext = gradeContext;
        _csvReader = csvReader ?? new CSVReader(); // Use default if csvReader is not provided
        Grades = _csvReader.ReadCSV<Grade>(filePath).ToList();
    }

    public IEnumerable<Grade> GetGrades()
    {
        return Grades;
    }

    public void AddGrade(Grade grade)
    {
        grade.GradeId = nextGradeId++.ToString();
        Grades.Add(grade);
        WriteDataToCsv();
    }

    public void UpdateGrade(Grade grade)
    {
        var existingGrade = Grades.FirstOrDefault(g => g.GradeId == grade.GradeId);
        if (existingGrade != null)
        {
            existingGrade.StudentNo = grade.StudentNo;
            existingGrade.CourseCode = grade.CourseCode;
            existingGrade.Score = grade.Score;
            WriteDataToCsv();
        }
    }

    public void DeleteGrade(string gradeId)
    {
        var grade = Grades.FirstOrDefault(g => g.GradeId == gradeId);
        if (grade != null)
        {
            Grades.Remove(grade);
            WriteDataToCsv();
        }
    }

    public Grade GetGrade(string gradeId)
    {
        return Grades.FirstOrDefault(g => g.GradeId == gradeId);
    }

    public IEnumerable<Grade> GetGradesByStudentId(string studentId)
    {
        return Grades.Where(g => g.StudentNo == studentId);
    }

    private void WriteDataToCsv()
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
        csv.WriteRecords(Grades);
    }
}

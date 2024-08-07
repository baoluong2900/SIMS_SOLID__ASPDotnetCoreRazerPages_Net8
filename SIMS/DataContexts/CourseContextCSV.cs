using CsvHelper;
using SIMS.Abstractions;
using SIMS.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class CourseContextCSV : ICourseService
{
    private int nextCourseCode = 1;
    private readonly ICourseService _courseContext;
    public List<Course> Courses { get; set; }
    private readonly string filePath = "DataCSV/CourseCSV.csv";
    private readonly ICSVReader _csvReader;

    public CourseContextCSV(ICourseService context)
    {
        _courseContext = context;
        Courses = _csvReader.ReadCSV<Course>(filePath).ToList();
    }

    public IEnumerable<Course> GetCourses()
    {
        return Courses;
    }

    public void AddCourse(Course course)
    {
        course.CourseCode = nextCourseCode++.ToString();
        Courses.Add(course);
        WriteDataToCsv();
    }

    public void UpdateCourse(Course course)
    {
        var existingCourse = Courses.FirstOrDefault(c => c.CourseCode == course.CourseCode);
        if (existingCourse != null)
        {
            existingCourse.CourseName = course.CourseName;
            existingCourse.StartDate = course.StartDate;
            existingCourse.EndDate = course.EndDate;
            existingCourse.Description = course.Description;
            WriteDataToCsv();
        }
    }

    public void DeleteCourse(string courseCode)
    {
        var course = Courses.FirstOrDefault(c => c.CourseCode == courseCode);
        if (course != null)
        {
            Courses.Remove(course);
            WriteDataToCsv();
        }
    }

    public Course GetCourse(string courseCode)
    {
        return Courses.FirstOrDefault(c => c.CourseCode == courseCode);
    }

    public List<Course> ReadCSV(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
        return csv.GetRecords<Course>().ToList();
    }

    private void WriteDataToCsv()
    {
        using var writer = new StreamWriter(filePath);
        using var csv = new CsvWriter(writer, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture));
        csv.WriteRecords(Courses);
    }
}

using SIMS.Model;

namespace SIMS.Abstractions
{
    public interface IStudentCourseService
    {
        IEnumerable<StudentCourse> GetStudentCourses();
        void AddStudentCourse(StudentCourse studentCourse);
        void UpdateStudentCourse(StudentCourse studentCourse);
        void DeleteStudentCourse(string studentCourseNo);
        StudentCourse GetStudentCourse(string studentCourseNo);
    }
}

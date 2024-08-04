using SIMS.Model;

namespace SIMS.Abstractions
{
    public interface ICourseService
    {
        IEnumerable<Course> GetCourses();
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(string courseId);
        Course GetCourse(string courseId);
    }
}

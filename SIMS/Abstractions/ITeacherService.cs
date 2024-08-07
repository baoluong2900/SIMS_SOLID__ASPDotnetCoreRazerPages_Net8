using SIMS.Model;

namespace SIMS.Abstractions
{
    public interface ITeacherService
    {
        IEnumerable<Teacher> GetTeachers();
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(string teacherNo);
        Teacher GetTeacher(string teacherNo);
    }
}

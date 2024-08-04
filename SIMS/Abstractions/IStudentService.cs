using SIMS.Model;

namespace SIMS.Abstractions
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudents();
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(string studentNo);
        Student GetStudent(string studentNo);
    }
}

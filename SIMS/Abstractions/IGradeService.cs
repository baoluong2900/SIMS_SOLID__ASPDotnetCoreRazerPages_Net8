using SIMS.Model;

namespace SIMS.Abstractions
{
    public interface IGradeService
    {
        IEnumerable<Grade> GetGrades();
        void AddGrade(Grade Grade);
        void UpdateGrade(Grade Grade);
        void DeleteGrade(string GradeId);
        Grade GetGrade(string GradeId);

        IEnumerable<Grade> GetGradesByStudentId(string studentId);
    }
}

namespace SIMS.Model
{
    public class Teacher
    {
        public string TeacherNo { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}

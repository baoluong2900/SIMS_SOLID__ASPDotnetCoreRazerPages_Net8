namespace SIMS.Model
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentNo { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string UrlHandle { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}

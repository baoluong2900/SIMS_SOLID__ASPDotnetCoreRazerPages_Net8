using SIMS.Abstractions;
using SIMS.Model;

namespace SIMS.DataContexts
{
    public class StudentContextCSV : IStudentService
    {
        private int nextStudentId = 1;

        public List<Student> Students { get; set; }

        private readonly string filePath;

        public StudentContextCSV(string filePath)
        {
            this.filePath = filePath;
            Students = ReadDataFromCsvAndUpdateId(filePath);
        }

        public List<Student> ReadDataFromCsvAndUpdateId(string filePath)
        {
            List<Student> students = new List<Student>();
            int nextStudentId = 1; // Reset the counter

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Skip the header line
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        if (values.Length >= 10)
                        {
                            Student student = new Student
                            {
                                StudentID = int.Parse(values[0]),
                                StudentNo = values[1],
                                LastName = values[2],
                                FirstName = values[3],
                                UrlHandle = values[4],
                                Email = values[5],
                                DateOfBirth = DateTime.Parse(values[6]),
                                UserName = values[7]
                            };

                            students.Add(student);

                            // Update nextStudentId if needed
                            if (student.StudentID >= nextStudentId)
                            {
                                nextStudentId = student.StudentID + 1;
                            }
                        }
                    }
                }
            }
            return students;
        }
        private void WriteDataToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write header
                writer.WriteLine("StudentID,StudentNo,LastName,FirstName,UrlHandle,Email,DateOfBirth,UserName,Password");

                // Write data rows
                foreach (var student in Students)
                {
                    writer.WriteLine($"{student.StudentID},{student.StudentNo},{student.LastName},{student.FirstName},{student.UrlHandle},{student.Email},{student.DateOfBirth:yyyy-MM-dd},{student.UserName}");
                }
            }
        }



        public void AddStudent(Student student)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudents()
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(Student student)
        {
            throw new NotImplementedException();
        }
    }
}

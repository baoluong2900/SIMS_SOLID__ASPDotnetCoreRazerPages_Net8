using SIMS.Abstractions;
using SIMS.Model;

namespace SIMS.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICSVReader _csvReader;
        private List<UserLogin> _userLogins;
        private readonly string _roles;
        private readonly string _filePath = "DataCSV/LoginUserCSV.csv";

        public AuthService(ICSVReader csvReader)
        {
            _csvReader = csvReader;
            _userLogins = _csvReader.ReadCSV<UserLogin>(_filePath).ToList();
        }


        public void Logout()
        {
            // Implement logout logic here if needed
        }

        public UserLogin Login(string username, string password)
        {
            var user = _userLogins.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}

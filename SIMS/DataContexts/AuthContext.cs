using SIMS.Abstractions;
using SIMS.Model;

namespace SIMS.DataContexts
{
    public class AuthContext: IAuthService
    {
        private readonly string filePath;
        public List<UserLogin> UserLogins { get; set; }


        public AuthContext(string filePath, string userName, string password)
        {
            this.filePath = filePath;
            UserLogins = ReadUserLoginFromCsv(filePath);
        }
        public List<UserLogin> ReadUserLoginFromCsv(string filePath)
        {
            List<UserLogin> userLogins = new List<UserLogin>();
            int nextUserId = 1; // Reset the counter

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
                            UserLogin userLogin = new UserLogin
                            {
                                UserName = values[0],
                                Password = values[1],
                                Role = values[2]
                            };

                            userLogins.Add(userLogin);
                        }
                    }
                }
            }
            return userLogins;
        }

        public string Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}

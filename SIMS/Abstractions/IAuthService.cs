using SIMS.Model;

namespace SIMS.Abstractions
{
    public interface IAuthService
    {
        UserLogin Login(string username, string password);
        void Logout();
    }
}

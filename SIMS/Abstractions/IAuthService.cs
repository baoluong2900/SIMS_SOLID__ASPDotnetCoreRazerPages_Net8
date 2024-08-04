namespace SIMS.Abstractions
{
    public interface IAuthService
    {
        string Login(string username, string password);
        void Logout();
    }
}

using System.Threading.Tasks;
using CommonModels;
using CommonModels.Login;

namespace AuthService.Login;
    public interface IAuth
{
    Task<bool> LoginAsync(string username, string password);
    Task<string> GenerateTokenAsync(string username);
    Task<bool> RegisterAsync(User model);
    Task<bool> ForgotPasswordAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
}
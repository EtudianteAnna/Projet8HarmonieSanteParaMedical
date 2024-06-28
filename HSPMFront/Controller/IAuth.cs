
namespace HarmonieSanteParamedical.ClientGateway.Controllers
{
    internal interface IAuth
    {
        Task GenerateTokenAsync(string username);
        Task<bool> LoginAsync(string username, string password);
    }
}
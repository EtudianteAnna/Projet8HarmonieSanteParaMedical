// IAppUser.cs
namespace CommonModels.Roles
{
    public interface IAppUser
    {
        // Méthode pour obtenir le nom d'utilisateur par ID
        string GetUserNameById(string id);

       
    }
}
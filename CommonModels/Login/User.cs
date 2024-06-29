namespace CommonModels.Login;

public class User
{
    public string Username { get; set; }      // Nom d'utilisateur
    public string Password { get; set; }      // Mot de passe
    public string Email { get; set; }         // Email de l'utilisateur
    public string Phone { get; set; }         // Téléphone de l'utilisateur
    public List<string> Roles { get; set; }   // Liste de rôles de l'utilisateur

    public bool RememberMe { get; set; }

}
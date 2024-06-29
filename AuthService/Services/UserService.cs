using System.Collections.Generic;
using System.Threading.Tasks;
using CommonModels;
using CommonModels.Login;

public class UserService : IUserService
{
    // Vous pouvez utiliser un DbContext pour interagir avec une base de données, ici nous utilisons une liste en mémoire pour simplifier.
    private readonly List<User> _users = new List<User>();

    public Task<User> GetUserByIdAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public Task<bool> UpdateUserAsync(int id, User user)
    {
        var existingUser = _users.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return Task.FromResult(false);
        }

        // Mettre à jour les propriétés de l'utilisateur
        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        // Ajouter ici toutes les autres propriétés à mettre à jour

        return Task.FromResult(true);
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return Task.FromResult(false);
        }

        _users.Remove(user);
        return Task.FromResult(true);
    }
}

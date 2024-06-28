// AppUserService.cs
using CommonModels.Roles;
using Microsoft.AspNetCore.Identity;

namespace CommonModels.Models.Roles
{
    public class AppUserService : IAppUser
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public string GetUserNameById(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            return user?.UserName;
        }

        // Implémentation d'autres méthodes nécessaires
    }
}
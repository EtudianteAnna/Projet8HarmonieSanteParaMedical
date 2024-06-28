using Microsoft.AspNetCore.Identity;

namespace CommonModels.Models.Roles
{
    public class AppUser : IdentityUser
    {

        public string Phone { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
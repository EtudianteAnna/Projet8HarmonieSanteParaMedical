using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using AuthService.Login;
using CommonModels;

namespace AuthService.Services
{
    public class AuthenService : IAuth
    {
        private readonly Dictionary<string, User> _users = new();

        public AuthenService()
        {
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (_users.ContainsKey(username) && _users[username].Password == password)
            {
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<string> GenerateTokenAsync(string username)
        {
            var user = _users[username];
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username)
            };

            // Ajouter chaque rôle en tant que réclamation
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<bool> RegisterAsync(User model)
        {
            if (_users.ContainsKey(model.Username))
            {
                return await Task.FromResult(false);
            }

            _users[model.Username] = model;
            return await Task.FromResult(true);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await Task.FromResult(FindUserByEmail(email));
            if (user == null) return false;

            // Génération du token de réinitialisation et envoi par email
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await Task.FromResult(FindUserByEmail(email));
            if (user == null) return false;

            user.Password = newPassword;
            return await Task.FromResult(true);
        }

        private User FindUserByEmail(string email)
        {
            foreach (var user in _users.Values)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }
    }
}

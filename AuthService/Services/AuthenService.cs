using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using CommonModels;
using CommonModels.Login;
using AuthService.Login; // Espace de noms pour les modèles partagés

namespace AuthService.Services
{
    public class AuthenService : IAuth
    {
        private readonly Dictionary<string, User> _users = new();
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public AuthenService(IConfiguration configuration)
        {
            _jwtKey = configuration["Jwt:Key"];
            _jwtIssuer = configuration["Jwt:Issuer"];
            _jwtAudience = configuration["Jwt:Audience"];
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
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Ajouter chaque rôle en tant que réclamation
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
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

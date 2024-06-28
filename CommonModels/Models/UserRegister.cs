namespace CommonModels
{
    public class UserRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public bool RememberMe { get; set; }
    }
}
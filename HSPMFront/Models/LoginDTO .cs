﻿namespace CommonModels.DTOs
{
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

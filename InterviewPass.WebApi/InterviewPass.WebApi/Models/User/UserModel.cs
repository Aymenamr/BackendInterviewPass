﻿namespace InterviewPass.WebApi.Models.User
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }

    }
}

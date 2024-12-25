using System.ComponentModel.DataAnnotations;

namespace InterviewPass.WebApi.Models.User
{
    public class UserModel
    {
        public string? Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }

        [Required]
        public string UserType { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Login can only contain letters, numbers, hyphens (-), and underscores (_).")]
        public string Login { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Phone { get; set; }

    }
}

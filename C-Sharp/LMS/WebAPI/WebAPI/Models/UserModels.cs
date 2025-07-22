using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserResponse User { get; set; }
        public string Token { get; set; }
    }

    public class UpdateRoleRequest
    {
        [Required]
        [StringLength(50)]
        public string Role { get; set; }
    }

    public class UpdateUserRequest
    {
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
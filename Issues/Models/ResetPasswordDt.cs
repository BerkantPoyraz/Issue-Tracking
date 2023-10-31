using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
    public record ResetPasswordDt
    {
        public String? UserName { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Passowrd is required")]
        public String? Password { get; init; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassowrd is required")]
        [Compare("Password",ErrorMessage ="Password And ConfirmPassword must be match")]
        public string? ConfirmPassword { get; init; }

    }
}

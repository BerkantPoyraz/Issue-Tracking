using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
    public record UserDt
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="UserName is Required")]
        public string? UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; init; }

        public HashSet<String> Roles { get; set; } = new HashSet<string>();
    }
}

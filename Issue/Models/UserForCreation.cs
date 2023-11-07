using System.ComponentModel.DataAnnotations;

namespace Issues.Models
{
    public record UserForCreation : UserDt
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public String Password { get; init; }
    }
}

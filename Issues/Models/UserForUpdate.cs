namespace Issues.Models
{
    public record UserForUpdate : UserDt
    {
        public HashSet<string> UserRoles { get; set; } = new HashSet<string>();
    }
}
